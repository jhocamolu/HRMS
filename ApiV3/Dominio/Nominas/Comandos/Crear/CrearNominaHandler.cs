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

namespace ApiV3.Dominio.Nominas.Comandos.Crear
{
    public class CrearNominaHandler : IRequestHandler<CrearNominaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearNominaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearNominaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Nomina nomina = new Nomina();
                nomina.TipoLiquidacionId = (int)request.TipoLiquidacionId;
                nomina.SubperiodoId = (int)request.SubperiodoId;
                nomina.Estado = EstadoNomina.Inicializada;

                // Consulta el número de los registros con el mismo tipo de liquidación. 

                var numeroTipoLiquidacion = contexto.Nominas.Where(x => x.TipoLiquidacionId == request.TipoLiquidacionId &&
                                                                       x.EstadoRegistro == EstadoRegistro.Activo)
                                                    .OrderByDescending(c => c.Id)
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



                var validaPeriodoContable = contexto.PeriodoContables.Where(x => x.Estado == EstadoPeriodoContable.Activo
                                                                                       && x.EstadoRegistro == EstadoRegistro.Activo)
                                                                    .Select(s => new { s.Id, s.Fecha })
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
                        SubPeriodo subPeriodo = contexto.SubPeriodos.Find(request.SubperiodoId);
                        foreach (var periodoContable in validaPeriodoContable)
                        {
                            var tipoLiquidacion = contexto.TipoLiquidaciones.Find(request.TipoLiquidacionId);
                            if (tipoLiquidacion.FechaManual == false)
                            {
                                //Calcula fecha Inicio y Finalizacion para la nómina
                                nomina.FechaInicio = FechaLiquidacionNomina.CalculaFechaInicioLiquidacionNomina(periodoContable.Fecha, subPeriodo.DiaInicial);
                                nomina.FechaFinal = FechaLiquidacionNomina.CalculaFechaFinalLiquidacionNomina(periodoContable.Fecha, subPeriodo.Dias, subPeriodo.DiaInicial);
                            }
                            else
                            {
                                nomina.FechaInicio = (DateTime)request.FechaInicio;
                                nomina.FechaFinal = (DateTime)request.FechaFinal;
                            }
                            nomina.PeriodoContableId = (int)periodoContable.Id;
                        }
                    }
                }

                contexto.Nominas.Add(nomina);
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
