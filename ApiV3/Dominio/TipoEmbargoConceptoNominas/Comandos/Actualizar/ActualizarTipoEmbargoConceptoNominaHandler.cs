using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoEmbargoConceptoNominas.Comandos.Actualizar
{
    public class ActualizarTipoEmbargoConceptoNominaHandler : IRequestHandler<ActualizarTipoEmbargoConceptoNominaRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;

        public ActualizarTipoEmbargoConceptoNominaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarTipoEmbargoConceptoNominaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Actualiza registro con tipo embargo concepto nómina.
                TipoEmbargoConceptoNomina tipoEmbargoConceptoNomina = this.contexto.TipoEmbargoConceptoNominas.Find(request.Id);
                tipoEmbargoConceptoNomina.TipoEmbargoId = (int)request.TipoEmbargoId;
                tipoEmbargoConceptoNomina.ConceptoNominaId = (int)request.ConceptoNominaId;
                tipoEmbargoConceptoNomina.MaximoEmbargarConcepto = (double)request.MaximoEmbargarConcepto;

                this.contexto.TipoEmbargoConceptoNominas.Update(tipoEmbargoConceptoNomina);

                await this.contexto.SaveChangesAsync();

                return CommandResult.Success(tipoEmbargoConceptoNomina);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
