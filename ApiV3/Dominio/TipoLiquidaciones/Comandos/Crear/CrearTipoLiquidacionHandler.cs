using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoLiquidaciones.Comandos.Crear
{
    public class CrearTipoLiquidacionHandler : IRequestHandler<CrearTipoLiquidacionRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearTipoLiquidacionHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearTipoLiquidacionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoLiquidacion tipoLiquidacion = new TipoLiquidacion
                {
                    Codigo = request.Codigo.ToUpper(),
                    Nombre = Texto.TipoOracion(request.Nombre),
                    TipoPeriodoId = request.TipoPeriodoId,
                    Descripcion = Texto.TipoOracion(request.Descripcion),
                    FechaManual = (bool)request.FechaManual,
                    Contabiliza = (bool)request.Contabiliza,
                    AplicaPila = (bool)request.AplicaPila,
                    Proceso = (TipoLiquidacionProceso)request.Proceso,
                    ConceptoNominaAgrupadorId = (int)request.ConceptoNominaAgrupadorId,
                    OperacionTotal = (OperacionTotalTipoLiqidacion)request.OperacionTotal
                };


                this.contexto.TipoLiquidaciones.Add(tipoLiquidacion);
                await this.contexto.SaveChangesAsync();

                if (request.ListaTipoLiquidacionModulos != null)
                {
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
