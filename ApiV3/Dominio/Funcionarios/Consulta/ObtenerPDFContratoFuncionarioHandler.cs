using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Servicios.Peticion;
using ApiV3.Support;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Funcionarios.Consulta
{
    public class ObtenerPDFContratoFuncionarioHandler : IRequestHandler<ObtenerPDFContratoFuncionarioRequest, CommandResult>
    {
        private readonly IPeticionService peticionService;
        private readonly IConfiguration configuration;
        private readonly NominaDbContext contexto;

        public ObtenerPDFContratoFuncionarioHandler(IPeticionService peticionService, IConfiguration configuration, NominaDbContext contexto)
        {
            this.peticionService = peticionService ?? throw new ArgumentNullException(nameof(peticionService));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        public async Task<CommandResult> Handle(ObtenerPDFContratoFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var host = configuration.GetValue<string>(Constants.ServiceNode.PDF);
                var url = configuration.GetValue<string>(Constants.Documentos.OBTENERCONTRATO);
                var dato = this.contexto.FuncionarioDatoActuales.FirstOrDefault(x => x.Id == request.FuncionarioId && x.ContratoId != null);
                if (dato != null)
                {
                    var contrato = contexto.Contratos.FirstOrDefault(x => x.Id == dato.ContratoId);
                    var tipocontrato = contexto.TipoContratos.FirstOrDefault(y => y.Id == contrato.TipoContratoId);

                    if (!string.IsNullOrEmpty(tipocontrato.DocumentoSlug))
                    {
                        var link = host + url.Replace("|grupo_slug|", dato.Contrato.TipoContrato.DocumentoSlug).Replace("|contrato_id|", dato.Contrato.Id.ToString());
                        if (url != null)
                        {

                            var response = await peticionService.Get(link);
                            var content = await response.Content.ReadAsStreamAsync();
                            return CommandResult.Success(content);
                        }
                        else
                        {
                            return CommandResult.Fail("Url no valida", 404);
                        }
                    }
                    else
                    {
                        return CommandResult.Fail("El tipo de contrato no tiene una plantilla asignada.", 400);
                    }

                }
                else
                {
                    return CommandResult.Fail("Funcionario no tiene contrato.", 404);
                }
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message, 500);
            }
        }
    }
}
