using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Nominas.Comandos.Actualizar
{
    public class ActualizarNominaHandler : IRequestHandler<ActualizarNominaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarNominaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarNominaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Nomina nomina = contexto.Nominas.Find(request.Id);
                // Consulta el número de los registros con el mismo tipo de liquidación. 
                if (nomina.TipoLiquidacionId != request.TipoLiquidacionId)
                {
                    var numeroTipoLiquidacion = contexto.Nominas.Where(x => x.TipoLiquidacionId == request.TipoLiquidacionId &&
                                                                           x.EstadoRegistro == EstadoRegistro.Activo)
                                                        .OrderByDescending(c => c.Numero)
                                                        .FirstOrDefault();

                    if (numeroTipoLiquidacion != null)
                    {
                        int numero = numeroTipoLiquidacion.Numero + 1;
                        nomina.Numero = numero;
                    }
                    else
                    {
                        int numero = 1;
                        nomina.Numero = numero;
                    }
                }

                nomina.TipoLiquidacionId = (int)request.TipoLiquidacionId;
                nomina.SubperiodoId = (int)request.SubperiodoId;

                var validaPeriodoContable = contexto.PeriodoContables.Where(x => x.Estado == EstadoPeriodoContable.Activo
                                                                                       && x.EstadoRegistro == EstadoRegistro.Activo)
                                                                    .Select(x => x.Fecha)
                                                                    .ToList();
                //Consulta la fecha desde el periodo contable 
                if (validaPeriodoContable.Count() == 0)
                {
                    return CommandResult.Fail("No es posible generar una liquidación, hasta que exista un período contable activo.", 404);
                }
                else
                {
                    if (validaPeriodoContable.Count() > 1)
                    {
                        return CommandResult.Fail("No es posible generar una liquidación. Existe más de un período contable activo.", 404);
                    }
                    else
                    {
                        var tipoLiquidacion = contexto.TipoLiquidaciones.Find(request.TipoLiquidacionId);
                        if (tipoLiquidacion.FechaManual == false)
                        {
                            SubPeriodo subPeriodo = contexto.SubPeriodos.Find(request.SubperiodoId);

                            //Calcula fecha Inicio y Finalizacion para la nómina
                            nomina.FechaInicio = FechaLiquidacionNomina.CalculaFechaInicioLiquidacionNomina(validaPeriodoContable[0], subPeriodo.DiaInicial);
                            nomina.FechaFinal = FechaLiquidacionNomina.CalculaFechaFinalLiquidacionNomina(validaPeriodoContable[0], subPeriodo.Dias, subPeriodo.DiaInicial);
                        }
                        else
                        {
                            nomina.FechaInicio = (DateTime)request.FechaInicio;
                            nomina.FechaFinal = (DateTime)request.FechaFinal;
                        }
                    }
                }


                contexto.Nominas.Update(nomina);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(nomina);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
