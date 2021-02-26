using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Servicios.Peticion;
using ApiV3.Support;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Autenticacion.Comandos.LoginAplicacion
{
    public class LoginAplicacionCommandHandler : IRequestHandler<LoginAplicacionCommand, CommandResult>
    {
        private readonly IPeticionService peticionService;
        private readonly IConfiguration configuration;
        private readonly NominaDbContext contexto;

        public LoginAplicacionCommandHandler(IPeticionService peticionService, IConfiguration configuration, NominaDbContext contexto)
        {
            this.peticionService = peticionService ?? throw new ArgumentNullException(nameof(peticionService));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        public async Task<CommandResult> Handle(LoginAplicacionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var url = configuration.GetValue<string>(GetLoginAplicacion());
                var response = await peticionService.Post(url, request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    JObject respuesta = new JObject();
                    respuesta["token"] = content;
                    return CommandResult.Success(respuesta);
                }
                else
                {
                    return CommandResult.Fail(response.ReasonPhrase, (int)response.StatusCode);
                }
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
        private static string GetLoginAplicacion()
        {
            return Constants.Peticion.LOGINAPLICACION;
        }
    }
}
