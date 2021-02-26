using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoLiquidaciones.Comandos.Actualizar
{
    public class ActualizarTipoLiquidacionHandler : IRequestHandler<ActualizarTipoLiquidacionRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarTipoLiquidacionHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarTipoLiquidacionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoLiquidacion tipoLiquidacion = this.contexto.TipoLiquidaciones.Find(request.Id);

                tipoLiquidacion.Codigo = request.Codigo.ToUpper();
                tipoLiquidacion.Nombre = Texto.TipoOracion(request.Nombre);
                tipoLiquidacion.TipoPeriodoId = request.TipoPeriodoId;
                tipoLiquidacion.Descripcion = Texto.TipoOracion(request.Descripcion);
                tipoLiquidacion.FechaManual = (bool)request.FechaManual;
                tipoLiquidacion.Contabiliza = (bool)request.Contabiliza;
                tipoLiquidacion.AplicaPila = (bool)request.AplicaPila;
                tipoLiquidacion.Proceso = (TipoLiquidacionProceso)request.Proceso;
                tipoLiquidacion.ConceptoNominaAgrupadorId = (int)request.ConceptoNominaAgrupadorId;
                tipoLiquidacion.OperacionTotal = (OperacionTotalTipoLiqidacion)request.OperacionTotal;

                this.contexto.TipoLiquidaciones.Update(tipoLiquidacion);
                await this.contexto.SaveChangesAsync();

                if (request.ListaTipoLiquidacionModulos != null)
                {
                    string tabla = typeof(TipoLiquidacionModulo).Name;
                    this.contexto.Database
                                 .ExecuteSqlRaw($"DELETE FROM {tabla} WHERE TipoLiquidacionId ={ request.Id}");

                    foreach (var item in request.ListaTipoLiquidacionModulos)
                    {
                        TipoLiquidacionModulo tipoLiquidacionModulo = new TipoLiquidacionModulo
                        {
                            TipoLiquidacionId = tipoLiquidacion.Id,
                            Modulo = item.Modulo
                        };
                        this.contexto.TipoLiquidacionModulos.Add(tipoLiquidacionModulo);
                        await this.contexto.SaveChangesAsync();
                    }
                }

                return CommandResult.Success(tipoLiquidacion);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
