using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.RangoUvts.Comandos.Eliminar
{
    public class EliminarRangoUvtHandler : IRequestHandler<EliminarRangoUvtRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarRangoUvtHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarRangoUvtRequest request, CancellationToken cancellationToken)
        {
            try
            {
                RangoUvt rangoUvt = await contexto.RangoUvts.FindAsync(request.Id);
                if (rangoUvt == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.RangoUvts.Remove(rangoUvt);
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
