using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoLiquidacionEstados.Comandos.Crear
{
    public class CrearTipoLiquidacionEstadoHandler : IRequestHandler<CrearTipoLiquidacionEstadoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearTipoLiquidacionEstadoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearTipoLiquidacionEstadoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoLiquidacionEstado liquidacionEstado = new TipoLiquidacionEstado
                {
                    TipoLiquidacionId = (int)request.TipoLiquidacionId,
                    EstadoContrato = (EstadoContrato)request.EstadoContrato,
                    EstadoFuncionario = (EstadoFuncionario)request.EstadoFuncionario

                };

                this.contexto.TipoLiquidacionEstados.Add(liquidacionEstado);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(liquidacionEstado);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
