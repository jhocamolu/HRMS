using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.LiquidacionConceptos.Comandos.Crear
{
    public class CrearTipoLiquidacionConceptoHandler : IRequestHandler<CrearTipoLiquidacionConceptoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearTipoLiquidacionConceptoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearTipoLiquidacionConceptoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoLiquidacionConcepto liquidacionConcepto = new TipoLiquidacionConcepto
                {
                    TipoliquidacionId = (int)request.TipoliquidacionId,
                    ConceptoNominaId = (int)request.ConceptoNominaId,
                    SubPeriodoId = (int)request.SubPeriodoId
                };
                if (request.TipoContratoId != 0)
                {
                    liquidacionConcepto.TipoContratoId = (int)request.TipoContratoId;
                }

                this.contexto.TipoLiquidacionConceptos.Add(liquidacionConcepto);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(liquidacionConcepto);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
