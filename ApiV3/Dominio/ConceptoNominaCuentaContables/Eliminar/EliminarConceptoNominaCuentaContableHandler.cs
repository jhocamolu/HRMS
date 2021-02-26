using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ConceptoNominaCuentaContables.Eliminar
{
    public class EliminarConceptoNominaCuentaContableHandler : IRequestHandler<
                                     EliminarConceptoNominaCuentaContableRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarConceptoNominaCuentaContableHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarConceptoNominaCuentaContableRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ConceptoNominaCuentaContable cuentaDebito = await this.contexto.ConceptoNominaCuentaContables
                                                                            .FindAsync(request.Id);
                if (cuentaDebito == null)
                {
                    CommandResult.Fail("No existe", 404);
                }
                this.contexto.ConceptoNominaCuentaContables.Remove(cuentaDebito);
                await contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
