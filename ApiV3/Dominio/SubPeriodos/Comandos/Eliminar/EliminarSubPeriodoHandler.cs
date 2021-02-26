using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.SubPeriodos.Comandos.Eliminar
{
    public class EliminarSubPeriodoHandler : IRequestHandler<EliminarSubPeriodoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarSubPeriodoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarSubPeriodoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                SubPeriodo subPeriodo = this.contexto.SubPeriodos.Find(request.Id);
                if (subPeriodo == null) return CommandResult.Fail("No existe", 404);

                this.contexto.SubPeriodos.Remove(subPeriodo);
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
