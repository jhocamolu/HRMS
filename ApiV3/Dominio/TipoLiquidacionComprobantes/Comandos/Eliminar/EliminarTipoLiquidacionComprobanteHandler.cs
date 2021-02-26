using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoLiquidacionComprobantes.Comandos.Eliminar
{
    public class EliminarTipoLiquidacionComprobanteHandler : IRequestHandler<EliminarTipoLiquidacionComprobanteRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarTipoLiquidacionComprobanteHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarTipoLiquidacionComprobanteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoLiquidacionComprobante tipoGastoViaje = this.contexto.TipoLiquidacionComprobantes.Find(request.Id);
            
                contexto.TipoLiquidacionComprobantes.Remove(tipoGastoViaje);
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
