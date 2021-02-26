using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoAusentismoConceptoNominas.Comandos.Crear
{
    public class CrearTipoAusentismoConceptoNominaHandler : IRequestHandler<CrearTipoAusentismoConceptoNominaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearTipoAusentismoConceptoNominaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearTipoAusentismoConceptoNominaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoAusentismoConceptoNomina conceptoNominaTipoAusentismo = new TipoAusentismoConceptoNomina
                {
                    TipoAusentismoId = (int)request.TipoAusentismoId,
                    ConceptoNominaId = (int)request.ConceptoNominaId,
                    CoberturaDesde = (int)request.CoberturaDesde,
                    CoberturaHasta = (int)request.CoberturaHasta
                };

                contexto.TipoAusentismoConceptoNominas.Add(conceptoNominaTipoAusentismo);

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
