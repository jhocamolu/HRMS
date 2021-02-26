using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de crear los registros en la entidad formaPago
/// </summary>

namespace ApiV3.Dominio.FormaPagos.Crear
{
    public class CrearFormaPagoHandler : IRequestHandler<CrearFormaPagoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public CrearFormaPagoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearFormaPagoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                FormaPago formaPago = new FormaPago
                {
                    Nombre = Texto.TipoOracion(request.Nombre)
                };

                this.contexto.FormaPagos.Add(formaPago);
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
