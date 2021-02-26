using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de actualizar registros en la entidad Operadores de pago
/// </summary>

namespace ApiV3.Dominio.OperadorPagos.Comando.Actualizar
{
    public class ActualizarOperadorPagoHandler : IRequestHandler<ActualizarOperadorPagoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ActualizarOperadorPagoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarOperadorPagoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                OperadorPago operadorPago = this.contexto.OperadorPagos.Find(request.Id);

                operadorPago.Nombre = Texto.TipoOracion(request.Nombre);
                operadorPago.PaginaWeb = request.PaginaWeb.ToLower();

                this.contexto.OperadorPagos.Update(operadorPago);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(operadorPago);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
