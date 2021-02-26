using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de realizar las validaciones para Actualizar registros en la entidad
/// formapago
/// </summary>

namespace ApiV3.Dominio.FormaPagos.Actualizar
{
    public class ActualizarFormaPagoHandler : IRequestHandler<ActualizarFormaPagoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ActualizarFormaPagoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarFormaPagoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                FormaPago formaPago = this.contexto.FormaPagos.Find(request.Id);
                formaPago.Nombre = Texto.TipoOracion(request.Nombre);

                this.contexto.FormaPagos.Update(formaPago);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(formaPago);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
