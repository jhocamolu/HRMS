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

namespace ApiV3.Dominio.TipoLiquidacionComprobantes.Comandos.Actualizar
{
    public class ActualizarTipoLiquidacionComprobanteHandler : IRequestHandler<ActualizarTipoLiquidacionComprobanteRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarTipoLiquidacionComprobanteHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarTipoLiquidacionComprobanteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoLiquidacionComprobante tipoLiquidacionComprobante = this.contexto.TipoLiquidacionComprobantes.Find(request.Id);
                tipoLiquidacionComprobante.TipoLiquidacionId = (int)request.TipoLiquidacionId;
                tipoLiquidacionComprobante.TipoComprobante = (TipoComprobante)request.TipoComprobante;
                tipoLiquidacionComprobante.CentroCostoId = (int)request.CentroCostoId;
                tipoLiquidacionComprobante.CuentaContableId = (int)request.CuentaContableId;
                tipoLiquidacionComprobante.Naturaleza = (NaturalezaContable)request.Naturaleza;

                contexto.TipoLiquidacionComprobantes.Update(tipoLiquidacionComprobante);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(tipoLiquidacionComprobante);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
