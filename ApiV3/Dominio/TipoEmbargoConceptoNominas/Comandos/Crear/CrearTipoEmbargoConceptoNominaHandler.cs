using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoEmbargoConceptoNominas.Comandos.Crear
{
    public class CrearTipoEmbargoConceptoNominaHandler : IRequestHandler<CrearTipoEmbargoConceptoNominaRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;

        public CrearTipoEmbargoConceptoNominaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearTipoEmbargoConceptoNominaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoEmbargoConceptoNomina tipoEmbargoConceptoNomina = new TipoEmbargoConceptoNomina();
                tipoEmbargoConceptoNomina.TipoEmbargoId = (int)request.TipoEmbargoId;
                tipoEmbargoConceptoNomina.ConceptoNominaId = (int)request.ConceptoNominaId;
                tipoEmbargoConceptoNomina.MaximoEmbargarConcepto = (double)request.MaximoEmbargarConcepto;

                this.contexto.TipoEmbargoConceptoNominas.Add(tipoEmbargoConceptoNomina);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(tipoEmbargoConceptoNomina);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.InnerException.Message);
            }
        }
    }
}
