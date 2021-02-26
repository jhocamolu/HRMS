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

namespace ApiV3.Dominio.Certificados.Consultas.SueldoContrato
{
    public class ObtenerSueldoCargoHandler : IRequestHandler<ObtenerSueldoCargoRequest, CommandResult>
    {
        private readonly IPeticionService peticionService;
        private readonly IConfiguration configuration;
        private readonly NominaDbContext contexto;

        public ObtenerSueldoCargoHandler(IPeticionService peticionService, IConfiguration configuration, NominaDbContext contexto)
        {
            this.peticionService = peticionService;
            this.configuration = configuration;
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ObtenerSueldoCargoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var ambiente = configuration.GetValue<string>(RutaArchivos.Ambiente()).Split(';')[1].Split('-')[1];
                string url = configuration.GetValue<string>(
                    RutaArchivos.UrlCertificado(ambiente, TipoCertificado.SUELDOCARGO));

                //String url = configuration.GetValue<string>(RutaArchivos.UrlPlantilla());
                if (url != null)
                {
                    url = url.Replace("id_Funcionario", request.Id);
                    var response = await peticionService.Get(url);
                    var content = await response.Content.ReadAsByteArrayAsync();
                    return CommandResult.Success(content);
                }
                else
                {
                    return CommandResult.Fail("Url no valida", 404);
                }
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message, 400);
            }
        }


    }
}