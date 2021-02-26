using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de actualizaciones parciales en la entidad Operadores de pago
/// </summary>

namespace ApiV3.Dominio.OperadorPagos.Comando.Parcial
{
    public class ParcialOperadorPagoHandler : IRequestHandler<ParcialOperadorPagoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ParcialOperadorPagoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialOperadorPagoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                OperadorPago operadorPago = this.contexto.OperadorPagos.Find(request.Id);

                if (request.Nombre != null) operadorPago.Nombre = Texto.TipoOracion(request.Nombre);
                if (request.PaginaWeb != null) operadorPago.PaginaWeb = request.PaginaWeb.ToLower();

                if (request.Activo != null)
                {
                    operadorPago.EstadoRegistro = EstadoRegistro.Activo;
                    if (request.Activo != true) operadorPago.EstadoRegistro = EstadoRegistro.Inactivo;
                }

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
