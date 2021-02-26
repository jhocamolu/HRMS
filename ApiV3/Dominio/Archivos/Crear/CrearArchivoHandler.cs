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

namespace ApiV3.Dominio.Archivos.Crear
{
    public class CrearArchivoHandler : IRequestHandler<CrearArchivoRequest, CommandResult>
    {
        private readonly IPeticionService peticionService;
        private readonly IConfiguration configuration;
        private readonly NominaDbContext contexto;

        public CrearArchivoHandler(IPeticionService peticionService, IConfiguration configuration, NominaDbContext contexto)
        {
            this.peticionService = peticionService;
            this.configuration = configuration;
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearArchivoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var hambiente = configuration.GetValue<string>(RutaArchivos.Ambiente()).Split(';')[1].Split('-')[1];

                string url = configuration.GetValue<string>(
                    RutaArchivos.UrlArchivos(hambiente, NombreArchivo.CREARARCHIVO));
                if (url != null)
                {
                    byte[] bytes = Convert.FromBase64String(request.Archivo);
                    var response = await peticionService.PostForm(url, bytes);
                    var content = response.Content.ReadAsStreamAsync().Result;
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
