using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de eliminar registros en la entidad Operadores de pago
/// </summary>

namespace ApiV3.Dominio.OperadorPagos.Comando.Eliminar
{
    public class EliminarOperadorPagoHandler : IRequestHandler<EliminarOperadorPagoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public EliminarOperadorPagoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarOperadorPagoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                OperadorPago operadorPago = await this.contexto.OperadorPagos.FindAsync(request.Id);
                if (operadorPago == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }

                this.contexto.OperadorPagos.Remove(operadorPago);
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
