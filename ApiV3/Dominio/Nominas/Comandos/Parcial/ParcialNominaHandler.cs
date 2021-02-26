using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Nominas.Comandos.Parcial
{
    public class ParcialNominaHandler : IRequestHandler<ParcialNominaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialNominaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialNominaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Nomina nomina = contexto.Nominas.Find(request.Id);
                nomina.TipoLiquidacionId = (int)request.TipoLiquidacionId;
                nomina.SubperiodoId = (int)request.SubperiodoId;
                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        nomina.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        nomina.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                //Consulta la fecha desde el periodo contable 
                PeriodoContable periodoContable = contexto.PeriodoContables.Find(request.PeriodoContableId);
                SubPeriodo subPeriodo = contexto.SubPeriodos.Find(request.SubperiodoId);
                if (periodoContable != null && subPeriodo != null)
                {
                    //Calcula fecha Inicio y Finalizacion para la nomina
                    nomina.FechaInicio = FechaLiquidacionNomina.CalculaFechaInicioLiquidacionNomina(periodoContable.Fecha, subPeriodo.DiaInicial);
                    nomina.FechaFinal = FechaLiquidacionNomina.CalculaFechaFinalLiquidacionNomina(periodoContable.Fecha, subPeriodo.Dias, subPeriodo.DiaInicial);
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
