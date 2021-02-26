using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.ProcedimientoDinamicos;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Formulas.Comandos.EjecutarFormulas
{
    public class EjecutarFormulaHandler : IRequestHandler<EjecutarFormulaRequest, CommandResult>
    {
        private readonly IDynamicProcedure procedure;
        private readonly NominaDbContext contexto;

        public EjecutarFormulaHandler(IDynamicProcedure procedure, NominaDbContext contexto)
        {
            this.procedure = procedure;
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EjecutarFormulaRequest request, CancellationToken cancellationToken)
        {
            string alias = "";
            try
            {
                List<ConceptoNomina> conceptos = contexto.ConceptoNominas.Where(c => c.ConceptoAgrupador == false).ToList();
                foreach (var concepto in conceptos)
                {
                    alias = concepto.Alias;
                    //if (alias == "Baseprimacesantiasmes")
                    //{
                    //    var a = "e";
                    //}
                    if (!string.IsNullOrEmpty(concepto.Formula))
                    {
                        var implementar = await procedure.StoredProcedure(concepto, concepto.Formula);
                        contexto.ConceptoNominas.Update(implementar);
                        contexto.SaveChanges();
                    }

                }
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail($"Concepto {alias}, error generado {e.Message}");
            }
        }
    }
}
