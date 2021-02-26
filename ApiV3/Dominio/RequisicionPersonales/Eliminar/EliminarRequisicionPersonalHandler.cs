using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.RequisicionPersonales.Eliminar
{
    public class EliminarRequisicionPersonalHandler : IRequestHandler<EliminarRequisicionPersonalRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarRequisicionPersonalHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarRequisicionPersonalRequest request, CancellationToken cancellationToken)
        {
            try
            {
                RequisicionPersonal requisicion = await contexto.RequisicionPersonales.FindAsync(request.Id);
                if (requisicion == null) return CommandResult.Fail("No existe", 404);

                this.contexto.RequisicionPersonales.Remove(requisicion);
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
