using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.LiquidacionConceptos.Comandos.Eliminar
{
    public class EliminarTipoLiquidacionConceptoHandler : IRequestHandler<EliminarTipoLiquidacionConceptoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarTipoLiquidacionConceptoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarTipoLiquidacionConceptoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var liquidacionConcepto = contexto.TipoLiquidacionConceptos.FirstOrDefault(x => x.Id == request.Id);
                this.contexto.TipoLiquidacionConceptos.Remove(liquidacionConcepto);
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
