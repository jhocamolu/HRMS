using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoAusentismoConceptoNominas.Comandos.Eliminar
{
    public class EliminarTipoAusentismoConceptoNominaHandler : IRequestHandler<EliminarTipoAusentismoConceptoNominaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarTipoAusentismoConceptoNominaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarTipoAusentismoConceptoNominaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoAusentismoConceptoNomina conceptoNominaTipoAusentismo = await contexto.TipoAusentismoConceptoNominas.FindAsync(request.Id);

                contexto.TipoAusentismoConceptoNominas.Remove(conceptoNominaTipoAusentismo);

                await contexto.SaveChangesAsync();
                return CommandResult.Success(conceptoNominaTipoAusentismo);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
