using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoEmbargos.Comandos.Crear
{
    public class CrearTipoEmbargoHandler : IRequestHandler<CrearTipoEmbargoRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;

        public CrearTipoEmbargoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearTipoEmbargoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoEmbargo tipoEmbargo = new TipoEmbargo();
                tipoEmbargo.Nombre = Texto.TipoOracion(request.Nombre.ToLower());
                tipoEmbargo.SalarioMinimoEmbargable = (bool)request.SalarioMinimoEmbargable;
                tipoEmbargo.Prioridad = (sbyte)request.Prioridad;
                this.contexto.TipoEmbargos.Add(tipoEmbargo);
                await this.contexto.SaveChangesAsync();

                TipoEmbargoConceptoNomina tipoEmbargoConceptoNomina = new TipoEmbargoConceptoNomina();
                tipoEmbargoConceptoNomina.TipoEmbargoId = tipoEmbargo.Id;
                tipoEmbargoConceptoNomina.ConceptoNominaId = (int)request.ConceptoNominaId;
                tipoEmbargoConceptoNomina.MaximoEmbargarConcepto = 0;

                this.contexto.TipoEmbargoConceptoNominas.Add(tipoEmbargoConceptoNomina);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(tipoEmbargo);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.InnerException.Message);
            }
        }

    }
}
