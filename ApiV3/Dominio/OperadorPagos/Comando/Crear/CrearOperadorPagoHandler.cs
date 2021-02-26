using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de crear registros en la entidad Operadores de pago
/// </summary>

namespace ApiV3.Dominio.OperadorPagos.Comando.Crear
{
    public class CrearOperadorPagoHandler : IRequestHandler<CrearOperadorPagoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public CrearOperadorPagoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(CrearOperadorPagoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                OperadorPago operadorPago = new OperadorPago
                {
                    Nombre = Texto.TipoOracion(request.Nombre),
                    PaginaWeb = request.PaginaWeb.ToLower()
                };

                this.contexto.OperadorPagos.Add(operadorPago);
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
