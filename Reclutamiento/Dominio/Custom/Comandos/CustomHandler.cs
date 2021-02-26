

using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Reclutamiento.Infraestructura.Resultados;
using Reclutamiento.Servicios.Peticion;
using Reclutamiento.Support;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Reclutamiento.Dominio.Custom.Comandos
{
    public class CustomHandler :Peticion, IRequestHandler<CustomRequest, CommandResult>
    {

        public CustomHandler(IConfiguration configuration, IPeticionService peticion)
        {
            this.configuration = configuration;
            this.peticion = peticion;
        }

        public async Task<CommandResult> Handle(CustomRequest request, CancellationToken cancellationToken)
        {
            try
            {
                dynamic data = await ObtenerServicios();
                if (data.IsSuccessStatusCode)
                {
                    var content = await data.Content.ReadAsStringAsync();
                    var respuesta = JObject.Parse(content);
                    return CommandResult.Success(respuesta);
                }
                else
                {
                    return CommandResult.Fail(data.ReasonPhrase, (int)data.StatusCode);
                }
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
        
    }
}
