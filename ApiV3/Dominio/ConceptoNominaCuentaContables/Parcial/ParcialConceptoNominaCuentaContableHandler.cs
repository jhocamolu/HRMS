using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ConceptoNominaCuentaContables.Parcial
{
    public class ParcialConceptoNominaCuentaContableHandler : IRequestHandler<
                                ParcialConceptoNominaCuentaContableRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ParcialConceptoNominaCuentaContableHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(ParcialConceptoNominaCuentaContableRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ConceptoNominaCuentaContable cuentaDebito = await this.contexto.ConceptoNominaCuentaContables
                                                                             .FindAsync(request.Id);
                //if (request.ConceptoNominaId != null) cuentaDebito.ConceptoNominaId = (int)request.ConceptoNominaId;
                //if (request.CentroCostoId != null) cuentaDebito.CentroCostoId = (int)request.CentroCostoId;
                //if (request.CuentaContableId != null) cuentaDebito.CuentaContableId = (int)request.CuentaContableId;
                if (request.Activo != null)
                {
                    cuentaDebito.EstadoRegistro = EstadoRegistro.Activo;
                    if (request.Activo == false)
                    {
                        cuentaDebito.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }

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
