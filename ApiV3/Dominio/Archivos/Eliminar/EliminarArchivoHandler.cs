using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Servicios.Peticion;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Archivos.Eliminar
{
    public class EliminarArchivoHandler : IRequestHandler<EliminarArchivoRequest, CommandResult>
    {
        private readonly IPeticionService peticionService;
        private readonly IConfiguration configuration;
        private readonly NominaDbContext contexto;

        public EliminarArchivoHandler(IPeticionService peticionService, IConfiguration configuration, NominaDbContext contexto)
        {
            this.peticionService = peticionService;
            this.configuration = configuration;
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarArchivoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var hambiente = configuration.GetValue<string>(RutaArchivos.Ambiente()).Split(';')[1].Split('-')[1];
                string url = configuration.GetValue<string>(
                    RutaArchivos.UrlArchivos(hambiente, NombreArchivo.ELIMINARARCHIVO)
                    );
                if (url != null)
                {
                    url = url + request.Id;
                    var response = await peticionService.Delete(url);
                    var content = await response.Content.ReadAsStringAsync();
                    var respuesta = JObject.Parse(content);
                    return CommandResult.Success(respuesta);
                }
                else
                {
                    return CommandResult.Fail("Url no valida", 404);
                }
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message, 500);
            }
        }
    }
}
