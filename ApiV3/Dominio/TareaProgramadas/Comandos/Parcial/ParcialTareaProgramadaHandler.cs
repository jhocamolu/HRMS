using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TareaProgramadas.Comandos.Parcial
{
    public class ParcialTareaProgramadaHandler : IRequestHandler<ParcialTareaProgramadaRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;
        public ParcialTareaProgramadaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialTareaProgramadaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TareaProgramada tareaProgramada = null;
                if (request.Id != null)
                {
                    tareaProgramada = this.contexto.TareaProgramadas.Find(request.Id);
                }
                if (request.Alias != null)
                {
                    tareaProgramada = this.contexto.TareaProgramadas.FirstOrDefault(x => x.Alias == request.Alias);
                }

                tareaProgramada.EnEjecucion = request.EnEjecucion;

                this.contexto.TareaProgramadas.Update(tareaProgramada);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(tareaProgramada);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
