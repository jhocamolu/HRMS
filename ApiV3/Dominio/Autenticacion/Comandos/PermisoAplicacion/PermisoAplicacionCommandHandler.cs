using ApiV3.Infraestructura.Resultados;
using ApiV3.Servicios.Peticion;
using ApiV3.Support;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Autenticacion.Comandos.PermisoAplicacion
{
    public class PermisoAplicacionCommandHandler : IRequestHandler<PermisoAplicacionCommand, CommandResult>
    {
        private readonly IPeticionService peticionService;
        private readonly IConfiguration configuration;

        public PermisoAplicacionCommandHandler(IPeticionService peticionService, IConfiguration configuration)
        {
            this.peticionService = peticionService;
            this.configuration = configuration;
        }

        public async Task<CommandResult> Handle(PermisoAplicacionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var url = configuration.GetValue<string>(GetPermisoAplicacion());
                var response = await peticionService.Post(url, request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
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

        private static string GetPermisoAplicacion()
        {
            return Constants.Peticion.PERMISOAPLICACION;
        }
    }
}
