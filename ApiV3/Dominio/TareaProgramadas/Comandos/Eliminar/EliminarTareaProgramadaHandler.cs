using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TareaProgramadas.Comandos.Eliminar
{
    public class EliminarTareaProgramadaHandler : IRequestHandler<EliminarTareaProgramadaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarTareaProgramadaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarTareaProgramadaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TareaProgramada tareaProgramada = await this.contexto.TareaProgramadas.FindAsync(request.Id);
                if (tareaProgramada == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.TareaProgramadas.Remove(tareaProgramada);
                await contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
