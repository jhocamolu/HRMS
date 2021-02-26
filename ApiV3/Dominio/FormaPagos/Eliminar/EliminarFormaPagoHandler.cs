using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
///  Clase encargada de  eliminar formas de pago.
/// </summary>

namespace ApiV3.Dominio.FormaPagos.Eliminar
{
    public class EliminarFormaPagoHandler : IRequestHandler<EliminarFormaPagoRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;
        public EliminarFormaPagoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarFormaPagoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                FormaPago formaPago = await this.contexto.FormaPagos.FindAsync(request.Id);
                if (formaPago == null) return CommandResult.Fail("No existe", 404);

                this.contexto.FormaPagos.Remove(formaPago);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
