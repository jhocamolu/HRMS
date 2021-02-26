
using MediatR;
using Microsoft.AspNetCore.Http;
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
using System.Threading;
using System.Threading.Tasks;

namespace Reclutamiento.Dominio.HojaDeVidaEstudios.Comandos.Crear
{
    public class CrearHojaDeVidaEstudioHandler : Peticion, IRequestHandler<CrearHojaDeVidaEstudioRequest, CommandResult>
    {

        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<UsuarioAplicacion> userManager;
        private readonly ReclutamientoDbContext contexto;

        public CrearHojaDeVidaEstudioHandler(IConfiguration configuration, IPeticionService peticion, IHttpContextAccessor httpContextAccessor, UserManager<UsuarioAplicacion> userManager, ReclutamientoDbContext contexto)
        {
            this.configuration = configuration;
            this.peticion = peticion;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearHojaDeVidaEstudioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var tokenPortal = InformacionToken.ObtenerTokenUsuario(httpContextAccessor);
                if (tokenPortal != "")
                {
                    var usuario = InformacionToken.ObtenerInformacionUsuario(tokenPortal, "unique_name", this.userManager);

                    var consultaHojaDeVida = this.contexto.HojaDeVidas.FirstOrDefault(x => x.Id == request.HojaDeVidaId);
                    if (usuario.UserName != consultaHojaDeVida.CorreoElectronicoPersonal)
                    {
                        return CommandResult.Fail("La información suministrada no es correcta, para realizar la acción.", 400);
                    }

                    var host = this.configuration.GetValue<string>(Constants.ServiceApi.GHESTIC);
                    var list = await this.EnviarServicios($"{host}/api/HojadeVidaEstudios", request, usuario.TokenGhestic);

                    if (list.IsSuccessStatusCode)
                    {
                        var content = await list.Content.ReadAsStringAsync();
                        var respuesta = JObject.Parse(content);
                        return CommandResult.Success(respuesta);
                    }
                    else
                    {
                        return CommandResult.Fail(list.ReasonPhrase, (int)list.StatusCode);
                    }
                }
                else
                {
                    return CommandResult.Fail("unauthorized", 401);
                }
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
