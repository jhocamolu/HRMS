using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoAusentismos.Comandos.Eliminar
{
    public class EliminarTipoAusentismoHandler : IRequestHandler<EliminarTipoAusentismoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarTipoAusentismoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarTipoAusentismoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoAusentismo tipoAusentismo = this.contexto.TipoAusentismos.Find(request.Id);
                if (tipoAusentismo == null) return CommandResult.Fail("No existe", 404);

                this.contexto.TipoAusentismos.Remove(tipoAusentismo);
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
