using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ConceptoNominaCuentaContables.Crear
{
    public class CrearConceptoNominaCuentaContableHandler : IRequestHandler<CrearConceptoNominaCuentaContableRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public CrearConceptoNominaCuentaContableHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(CrearConceptoNominaCuentaContableRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ConceptoNominaCuentaContable cuentaDebito = new ConceptoNominaCuentaContable
                {
                    ConceptoNominaId = (int)request.ConceptoNominaId,
                    CentroCostoId = request.CentroCostoId,
                    CuentaContableId = (int)request.CuentaContableId
                };

                this.contexto.ConceptoNominaCuentaContables.Add(cuentaDebito);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(cuentaDebito);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
