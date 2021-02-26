using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ConceptoNominas.Comandos.Reordenar
{
    public class ReordenarConceptoNominaHandler : IRequestHandler<ReordenarConceptoNominaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ReordenarConceptoNominaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ReordenarConceptoNominaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ConceptoNomina conceptoPrincipal = await contexto.ConceptoNominas.FirstOrDefaultAsync(x => x.Id == request.Id);

                ConceptoNomina conceptoCambioOrden = await contexto.ConceptoNominas.FirstOrDefaultAsync(y => y.Id == request.ConceptoNominaId);

                int ordenCambia = conceptoCambioOrden.Orden;

                List<ConceptoNomina> cambios = new List<ConceptoNomina>();
                if (request.Condicion == CondicionConceptoNomina.DespuesDe)
                {
                    var despues = contexto.ConceptoNominas.Where(c => c.Orden > conceptoCambioOrden.Orden)
                                                          .OrderBy(c => c.Orden)
                                                          .ToList();
                    foreach (var item in despues)
                    {
                        item.Orden = item.Orden + 1;
                        cambios.Add(item);
                    }
                    contexto.ConceptoNominas.UpdateRange(cambios);
                    await contexto.SaveChangesAsync();

                    conceptoPrincipal.Orden = conceptoCambioOrden.Orden + 1;
                }
                else if (request.Condicion == CondicionConceptoNomina.AntesDe)
                {
                    var antes = contexto.ConceptoNominas.Where(c => c.Orden >= conceptoCambioOrden.Orden)
                                                          .OrderBy(c => c.Orden)
                                                          .ToList();
                    foreach (var item in antes)
                    {
                        item.Orden = item.Orden + 1;
                        cambios.Add(item);
                    }
                    contexto.ConceptoNominas.UpdateRange(cambios);
                    await contexto.SaveChangesAsync();

                    conceptoPrincipal.Orden = ordenCambia;
                }

                contexto.ConceptoNominas.Update(conceptoPrincipal);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(conceptoPrincipal);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
