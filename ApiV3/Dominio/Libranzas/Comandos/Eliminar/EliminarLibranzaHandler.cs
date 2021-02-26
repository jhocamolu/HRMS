using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Libranzas.Comandos.Eliminar
{
    public class EliminarLibranzaHandler : IRequestHandler<EliminarLibranzaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarLibranzaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarLibranzaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Libranza libranza = await this.contexto.Libranzas.FindAsync(request.Id);
                if (libranza == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.Libranzas.Remove(libranza);
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
