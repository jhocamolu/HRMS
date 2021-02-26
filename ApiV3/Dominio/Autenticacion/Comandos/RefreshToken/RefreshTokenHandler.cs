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

namespace ApiV3.Dominio.Autenticacion.Comandos.RefreshToken
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, CommandResult>
    {
        private readonly IPeticionService peticionService;
        private readonly IConfiguration configuration;
        private readonly NominaDbContext contexto;

        public RefreshTokenHandler(IPeticionService peticionService, IConfiguration configuration, NominaDbContext contexto)
        {
            this.peticionService = peticionService ?? throw new ArgumentNullException(nameof(peticionService));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        public async Task<CommandResult> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var url = configuration.GetValue<string>(Constants.Peticion.REFRESHTOKEN);
                var response = await peticionService.Post(url, request);
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var respuesta = JObject.Parse(content);
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
    }
}
