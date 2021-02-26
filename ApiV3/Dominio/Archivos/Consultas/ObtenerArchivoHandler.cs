using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Servicios.Peticion;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Archivos.Consultas
{
    public class ObtenerArchivoHandler : IRequestHandler<ObtenerArchivoRequest, CommandResult>
    {
        private readonly IPeticionService peticionService;
        private readonly IConfiguration configuration;
        private readonly NominaDbContext contexto;

        public ObtenerArchivoHandler(IPeticionService peticionService, IConfiguration configuration, NominaDbContext contexto)
        {
            this.peticionService = peticionService;
            this.configuration = configuration;
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ObtenerArchivoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var hambiente = configuration.GetValue<string>(RutaArchivos.Ambiente()).Split(';')[1].Split('-')[1];
                string url = configuration.GetValue<string>(
                    RutaArchivos.UrlArchivos(hambiente, NombreArchivo.OBTENERARCHIVO)
                    );
                if (url != null)
                {
                    url = url + request.Id;
                    var response = await peticionService.Get(url);
                    var content = await response.Content.ReadAsStreamAsync();
                    return CommandResult.Success(content);
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
