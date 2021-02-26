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

namespace ApiV3.Dominio.TipoEmbargos.Comandos.Parcial
{
    public class ParcialTipoEmbargoHandler : IRequestHandler<ParcialTipoEmbargoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialTipoEmbargoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialTipoEmbargoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoEmbargo tipoEmbargos = this.contexto.TipoEmbargos.Find(request.Id);

                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        tipoEmbargos.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        tipoEmbargos.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                if (!String.IsNullOrEmpty(request.Nombre))
                {
                    tipoEmbargos.Nombre = Texto.TipoOracion(request.Nombre.ToLower());
                }
                if (request.SalarioMinimoEmbargable != null)
                {
                    tipoEmbargos.SalarioMinimoEmbargable = (bool)request.SalarioMinimoEmbargable;
                }

                this.contexto.TipoEmbargos.Update(tipoEmbargos);
                await this.contexto.SaveChangesAsync();

                if (request.ConceptoNominaId != null)
                {
                    var tipoEmbargoConceptoNominas = from te in contexto.TipoEmbargoConceptoNominas
                                                     join cn in contexto.ConceptoNominas on te.ConceptoNominaId equals cn.Id
                                                     where cn.ClaseConceptoNomina == ClaseConceptoNomina.Deduccion
                                                     && te.TipoEmbargoId == tipoEmbargos.Id
                                                     select te;

                    foreach (var item in tipoEmbargoConceptoNominas)
                    {
                        if (item.ConceptoNominaId != request.ConceptoNominaId)
                        {
                            //Actualiza registro con tipo embargo concepto nómina para la clase de concepto deducción.
                            TipoEmbargoConceptoNomina tipoEmbargoConceptoNomina = this.contexto.TipoEmbargoConceptoNominas.Find(item.Id);
                            tipoEmbargoConceptoNomina.TipoEmbargoId = tipoEmbargos.Id;
                            tipoEmbargoConceptoNomina.ConceptoNominaId = (int)request.ConceptoNominaId;
                            this.contexto.TipoEmbargoConceptoNominas.Update(tipoEmbargoConceptoNomina);
                        }
                    }

                }
                await this.contexto.SaveChangesAsync();

                this.contexto.TipoEmbargos.Update(tipoEmbargos);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(tipoEmbargos);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }


    }
}
