using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoAusentismoConceptoNominas.Comandos.Actualizar
{
    public class ActualizarTipoAusentismoConceptoNominaHandler : IRequestHandler<ActualizarTipoAusentismoConceptoNominaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarTipoAusentismoConceptoNominaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarTipoAusentismoConceptoNominaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoAusentismoConceptoNomina conceptoNominaTipoAusentismo = await contexto.TipoAusentismoConceptoNominas.FindAsync(request.Id);
                conceptoNominaTipoAusentismo.TipoAusentismoId = (int)request.TipoAusentismoId;
                conceptoNominaTipoAusentismo.ConceptoNominaId = (int)request.ConceptoNominaId;
                conceptoNominaTipoAusentismo.CoberturaDesde = (int)request.CoberturaDesde;
                conceptoNominaTipoAusentismo.CoberturaHasta = (int)request.CoberturaHasta;

                contexto.TipoAusentismoConceptoNominas.Update(conceptoNominaTipoAusentismo);

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
