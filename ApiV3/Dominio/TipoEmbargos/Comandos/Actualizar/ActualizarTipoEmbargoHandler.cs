using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoEmbargos.Comandos.Actualizar
{
    public class ActualizarTipoEmbargoHandler : IRequestHandler<ActualizarTipoEmbargoRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;

        public ActualizarTipoEmbargoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarTipoEmbargoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoEmbargo tipoEmbargo = this.contexto.TipoEmbargos.Find(request.Id);

                tipoEmbargo.Nombre = Texto.TipoOracion(request.Nombre.ToLower());
                tipoEmbargo.SalarioMinimoEmbargable = (bool)request.SalarioMinimoEmbargable;
                tipoEmbargo.Prioridad = (sbyte)request.Prioridad;
                this.contexto.TipoEmbargos.Update(tipoEmbargo);
                await this.contexto.SaveChangesAsync();

                var tipoEmbargoConceptoNominas = from te in contexto.TipoEmbargoConceptoNominas
                                                 join cn in contexto.ConceptoNominas on te.ConceptoNominaId equals cn.Id
                                                 where cn.ClaseConceptoNomina == ClaseConceptoNomina.Deduccion
                                                 && te.TipoEmbargoId == tipoEmbargo.Id
                                                 select te;

                foreach (var item in tipoEmbargoConceptoNominas)
                {
                    if (item.ConceptoNominaId != request.ConceptoNominaId)
                    {
                        //Actualiza registro con tipo embargo concepto nómina para la clase de concepto deducción.
                        TipoEmbargoConceptoNomina tipoEmbargoConceptoNomina = this.contexto.TipoEmbargoConceptoNominas.Find(item.Id);
                        tipoEmbargoConceptoNomina.TipoEmbargoId = tipoEmbargo.Id;
                        tipoEmbargoConceptoNomina.ConceptoNominaId = (int)request.ConceptoNominaId;
                        this.contexto.TipoEmbargoConceptoNominas.Update(tipoEmbargoConceptoNomina);
                    }
                }
                await this.contexto.SaveChangesAsync();

                return CommandResult.Success(tipoEmbargo);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
