using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Candidatos.Comandos.Eliminar
{
    public class EliminarCandidatoHandler : IRequestHandler<EliminarCandidatoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarCandidatoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarCandidatoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Candidato candidato = await contexto.Candidatos.FindAsync(request.Id);
                if (candidato == null) return CommandResult.Fail("No existe", 404);

                this.contexto.Candidatos.Remove(candidato);
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