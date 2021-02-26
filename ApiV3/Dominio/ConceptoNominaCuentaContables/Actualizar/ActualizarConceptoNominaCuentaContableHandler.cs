using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ConceptoNominaCuentaContables.Actualizar
{
    public class ActualizarConceptoNominaCuentaContableHandler : IRequestHandler<
                                        ActualizarConceptoNominaCuentaContableRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ActualizarConceptoNominaCuentaContableHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(ActualizarConceptoNominaCuentaContableRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ConceptoNominaCuentaContable cuentaDebito = await this.contexto.ConceptoNominaCuentaContables
                                                                             .FindAsync(request.Id);
                cuentaDebito.ConceptoNominaId = (int)request.ConceptoNominaId;
                cuentaDebito.CentroCostoId = request.CentroCostoId;
                cuentaDebito.CuentaContableId = (int)request.CuentaContableId;


                this.contexto.ConceptoNominaCuentaContables.Update(cuentaDebito);
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
