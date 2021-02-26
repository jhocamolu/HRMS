using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoPeriodos.Comandos.Eliminar
{
    public class EliminarTipoPeriodoHandler : IRequestHandler<EliminarTipoPeriodoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarTipoPeriodoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarTipoPeriodoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoPeriodo tipoPeriodo = this.contexto.TipoPeriodos.Find(request.Id);
                if (tipoPeriodo == null) return CommandResult.Fail("No existe", 404);

                this.contexto.TipoPeriodos.Remove(tipoPeriodo);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
