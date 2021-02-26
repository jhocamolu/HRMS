using ApiV3.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Reclutamiento.Infraestructura.DbContexto;
using Reclutamiento.Infraestructura.Resultados;
using Reclutamiento.Infraestructura.Utilidades;
using Reclutamiento.Models;
using Reclutamiento.Servicios.Peticion;
using Reclutamiento.Support;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Reclutamiento.Dominio.Autenticacion.Login
{
    public class LoginAutenticacionHandler : GenerarToken, IRequestHandler<LoginAutenticacionRequest, CommandResult>
    {
        protected IConfiguration configuration;
        private readonly SignInManager<UsuarioAplicacion> signInManager;
        private readonly UserManager<UsuarioAplicacion> userManager;
        private readonly IPeticionService peticionService;
        private readonly ReclutamientoDbContext contexto;

        public LoginAutenticacionHandler(IConfiguration configuration, SignInManager<UsuarioAplicacion> signInManager, UserManager<UsuarioAplicacion> userManager, IPeticionService peticionService, ReclutamientoDbContext contexto)
        {
            this.configuration = configuration;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.peticionService = peticionService;
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(LoginAutenticacionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await signInManager.PasswordSignInAsync(request.CorreoElectronicoPersonal, request.Clave, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var nombre = "";
                    HojaDeVida informacionHojaDeVida = this.contexto.HojaDeVidas.FirstOrDefault(x=> x.CorreoElectronicoPersonal == request.CorreoElectronicoPersonal);
                    if (informacionHojaDeVida != null)
                    {
                        nombre = informacionHojaDeVida.PrimerNombre + ' ' + informacionHojaDeVida.PrimerApellido;
                    }
                    else {
                        return CommandResult.Fail("No se encontró hoja de vida creada con el correo eléctronico suministrado.",400);
                    }
                    
                    //Autenticacion por el portal
                    var usuarioPortal = new UsuarioPortal { Usuario = request.CorreoElectronicoPersonal, Clave = request.Clave };
                    UsuarioToken generarToken = ConstruirToken(usuarioPortal, nombre, informacionHojaDeVida.NumeroDocumento, configuration);
                    
                    //Autenticacion ghestic
                    var url = configuration.GetValue<string>(Constants.ServiceApi.GHESTIC) + "/api/Autenticacion/Login";

                    object parametros = new
                    {
                        Cedula = configuration.GetValue<string>(Constants.UsuarioPortal.CEDULA),
                        Clave = configuration.GetValue<string>(Constants.UsuarioPortal.CLAVE),
                    };
                    var response = await peticionService.Post(url, parametros,"");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var respuesta = JObject.Parse(content);
                    
                        var jwtToken = respuesta.Value<String>("token");
                        if (jwtToken != null)
                        {
                            var user = await userManager.FindByNameAsync(request.CorreoElectronicoPersonal);
                            user.TokenGhestic = jwtToken;
                            await userManager.UpdateAsync(user);
                        }
                    }
                    else
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                        {
                            return CommandResult.Fail(response.ReasonPhrase, 400);
                        }
                        else
                        {
                            return CommandResult.Fail(response.ReasonPhrase);
                        }
                    }
                    var urlImagen = informacionHojaDeVida.Adjunto;
                    dynamic resultado = new  {
                        generarToken.Token,
                        urlImagen
                    };
                    return CommandResult.Success(resultado);
                }
                else
                {
                    return CommandResult.Fail("Tu usuario y/o contraseña son incorrectos.", 400);
                }
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
       
    }
}
