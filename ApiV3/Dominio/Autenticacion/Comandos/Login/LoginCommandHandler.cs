using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Servicios.Peticion;
using ApiV3.Support;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Autenticacion.Comandos.Login
{
    public class LoginCommandHandler : Peticion, IRequestHandler<LoginCommand, CommandResult>
    {

        private readonly IPeticionService peticionService;
        private readonly NominaDbContext contexto;

        public LoginCommandHandler(IPeticionService peticionService, IConfiguration configuration, NominaDbContext contexto)
        {
            this.peticionService = peticionService ?? throw new ArgumentNullException(nameof(peticionService));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        public async Task<CommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var url = configuration.GetValue<string>(GetLogin());


                var response = await peticionService.Post(url, request);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var respuesta = JObject.Parse(content);

                    var error = respuesta.Value<String>("error");
                    if (error == "Usuario y/o Clave Incorrecta")
                    {
                        respuesta.Remove("error");
                        respuesta.Add("error", "Tu usuario y/o contraseña son incorrectos.");
                    }

                    var cedula = respuesta.Value<String>("cedula");

                    if (cedula != null)
                    {
                        var funcionario = contexto.Funcionarios.FirstOrDefault(x => x.NumeroDocumento == cedula);
                        if (funcionario != null)
                        {
                            if (funcionario.PrimerNombre != null)
                            {
                                respuesta.Add("nombre", funcionario.PrimerNombre + " " + funcionario.PrimerApellido);
                            }
                            if (funcionario.Adjunto != null)
                            {

                                var host = this.configuration.GetValue<string>(Constants.ServiceNode.ARCHIVOS);
                                host = $"{host}/v1/bucket/download?document_id=" + funcionario.Adjunto;

                                var data = await peticionService.Get(host);

                                if (data.IsSuccessStatusCode)
                                {
                                    var urlImagen = $"v1/bucket/download?document_id=" + funcionario.Adjunto;
                                    respuesta.Add("urlImagen", urlImagen);
                                }
                                else
                                {
                                    respuesta.Add("urlImagen", null);
                                }
                            }
                        }
                        else
                        {
                            respuesta.Add("nombre", respuesta.Value<String>("usuario"));
                            respuesta.Add("urlImagen", null);
                        }
                    }
                    respuesta.Remove("cedula");
                    respuesta.Remove("usuario");

                    return CommandResult.Success(respuesta);

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
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }

        }

        private static string GetLogin()
        {
            return Constants.Peticion.LOGIN;
        }
    }
}
