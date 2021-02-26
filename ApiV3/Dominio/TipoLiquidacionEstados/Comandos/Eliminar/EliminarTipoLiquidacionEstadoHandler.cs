using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoLiquidacionEstados.Comandos.Eliminar
{
    public class EliminarTipoLiquidacionEstadoHandler : IRequestHandler<EliminarTipoLiquidacionEstadoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarTipoLiquidacionEstadoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarTipoLiquidacionEstadoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var liquidacionEstado = contexto.TipoLiquidacionEstados.FirstOrDefault(x => x.Id == request.Id);
                this.contexto.TipoLiquidacionEstados.Remove(liquidacionEstado);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(liquidacionEstado);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
