using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Graficas;
using ApiV3.Infraestructura.Repositorio;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Nominas.Comandos.Graficas
{
    public class GraficasNominaHandler : IRequestHandler<GraficasNominaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IReadOnlyRepository repositorio;
        private readonly IGraficaServices graficas;

        const int TOP = 6;

        public GraficasNominaHandler(NominaDbContext contexto, IGraficaServices graficas, IReadOnlyRepository repositorio)
        {
            this.contexto = contexto;
            this.graficas = graficas;
            this.repositorio = repositorio;
        }

        public async Task<CommandResult> Handle(GraficasNominaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Dictionary<string, object> objetos = new Dictionary<string, object>();

                var nomina = await contexto.Nominas.FirstOrDefaultAsync(n => n.Id == request.Id);
                if (nomina != null && (nomina.Estado == EstadoNomina.Inicializada || nomina.Estado == EstadoNomina.Modificada || nomina.Estado == EstadoNomina.EnLiquidacion))
                {
                    return CommandResult.Fail("La nomina no se encuentra en estado liquidada");
                }

                var tipoliquidacion = contexto.TipoLiquidaciones.FirstOrDefault(x => x.Id == nomina.TipoLiquidacionId);

                // Grafica de barras
                var datosGrafica1 = repositorio.Query($@"SELECT * FROM [dbo].[UFT_ObtenerHistoricoNomina] (
                                                    {TOP}
                                                  , '{nomina.Id}')
                                                ");
                List<string> labelGrafica1 = new List<string>();
                List<double> dataGrafica1 = new List<double>();
                double sumatoria = 0;
                if (datosGrafica1.Any())
                {
                    foreach (var item in datosGrafica1.ToList())
                    {
                        labelGrafica1.Add($"{item.Periodo}");
                        dataGrafica1.Add((double)item.Total);
                        sumatoria = sumatoria + (double)item.Total;
                    }
                }

                double total = dataGrafica1.Count() == 0 ? 0 : sumatoria / dataGrafica1.Count();

                object grafica1 = graficas.GraficaBasica("bar", "Ultimas Liquidaciones", labelGrafica1, dataGrafica1, "Promedio", total.ToString(), null);
                objetos.Add("bar", grafica1);

                // widget para Colaboradores
                var widget1 = contexto.NominaFuncionarios.Where(f => f.NominaId == nomina.Id && f.Estado == EstadoNominaFuncionario.Liquidado);
                objetos.Add("Widget1", graficas.Widget("Colaboradores", widget1.Count().ToString(), null, null, null));

                // widget para Colaboradores con neto a pagar en cero
                //var widget2 = contexto.NominaFuncionarios.Include(x => x.Funcionario).Where(f => f.NominaId == nomina.Id && f.NetoPagar == 0 && f.Estado == EstadoNominaFuncionario.Liquidado);

                var widget2 = (from nf in contexto.NominaFuncionarios
                               join f in contexto.FuncionarioDatoActuales.Include(x => x.Cargo) on nf.FuncionarioId equals f.Id
                               where nf.NominaId == nomina.Id && nf.NetoPagar == 0 && nf.Estado == EstadoNominaFuncionario.Liquidado
                               select new { Id = f.Id, NumeroDocumento = f.NumeroDocumento, CriterioBusqueda = f.CriterioBusqueda, Cargo = f.Cargo.Nombre }
                              ).ToList();


                objetos.Add("Widget2", graficas.Widget("Colaboradores con neto a pagar en cero", widget2.Count().ToString(), null, null, widget2));


                // widget para Colaboradores con neto a pagar negativo
                //var widget3 = contexto.NominaFuncionarios.Include(x => x.Funcionario).Include(x => x.Funcionario).Where(f => f.NominaId == nomina.Id && f.NetoPagar < 0 && f.Estado == EstadoNominaFuncionario.Liquidado);

                var widget3 = (from nf in contexto.NominaFuncionarios
                               join f in contexto.FuncionarioDatoActuales.Include(x => x.Cargo) on nf.FuncionarioId equals f.Id
                               where nf.NominaId == nomina.Id && nf.NetoPagar < 0 && nf.Estado == EstadoNominaFuncionario.Liquidado
                               select new { Id = f.Id, NumeroDocumento = f.NumeroDocumento, CriterioBusqueda = f.CriterioBusqueda, Cargo = f.Cargo.Nombre }
                              ).ToList();

                objetos.Add("Widget3", graficas.Widget("Colaboradores con neto a pagar negativo", widget3.Count().ToString(), null, null, widget3));

                // widget para Colaboradores no incluidos en la liquidación de la nómina
                var widget4 = repositorio.Query($@"DECLARE @NominaId INT = '{nomina.Id}';

                            DECLARE @tipoLiquidacionNomina INT=
                            (
                                SELECT n.TipoLiquidacionId
                                FROM dbo.Nomina n
                                WHERE n.Id = @NominaId
                            );
                            DECLARE @PeriodoNomina INT=
                            (
                                SELECT n.PeriodoContableId
                                FROM dbo.Nomina n
                                WHERE n.Id = @NominaId
                            );
                            DECLARE @FechaNomina DATE=
                            (
                                SELECT n.FechaInicio
                                FROM dbo.Nomina n
                                WHERE n.Id = @NominaId
                            );
                            SELECT DISTINCT 
                                   vfda.Id,
                                   vfda.NumeroDocumento,
                                   vfda.CriterioBusqueda,
                                   car.Nombre AS 'Cargo'
                            FROM dbo.Nomina n
                                 INNER JOIN dbo.NominaFuncionario nf ON n.Id = nf.NominaId
                                                                        AND n.TipoLiquidacionId = @tipoLiquidacionNomina
                                                                        AND n.PeriodoContableId = @PeriodoNomina
                                                                        AND n.FechaInicio = @FechaNomina
                                                                        AND nf.EstadoRegistro =
                            (
                                SELECT vce.AUDITORIA_ACTIVO
                                FROM util.VW_ConstanteEstado vce
                            )
                                 INNER JOIN dbo.TipoLiquidacion tl ON n.TipoLiquidacionId = tl.Id
                                 RIGHT OUTER JOIN dbo.VW_FuncionarioDatoActual vfda ON nf.FuncionarioId = vfda.Id
                                                                                       AND vfda.Estado IN(SELECT tle.EstadoFuncionario
                                                                                                          FROM dbo.TipoLiquidacionEstado tle
                                                                                                          WHERE tle.TipoLiquidacionId = tl.Id)
                                 INNER JOIN Cargo car ON vfda.CargoId = car.Id
                            WHERE vfda.Id IS NOT NULL
                                  AND nf.id IS NULL;");

                objetos.Add("Widget4", graficas.Widget("Colaboradores no incluidos en la liquidación de la nómina", widget4.Count().ToString(), null, null, widget4));

                // widget para Centros de costo pendientes por parametrizar
                var widget5 = "";

                objetos.Add("Widget5", graficas.Widget("Centros de costo pendientes por parametrizar", "0", null, null, null));


                // widget para total neto pagar
                var widget6 = contexto.NominaDetalles.Where(x => x.NominaFuncionario.Nomina.Id == nomina.Id && x.NominaFuncionario.Estado == EstadoNominaFuncionario.Liquidado && x.ConceptoNomina.Id == tipoliquidacion.ConceptoNominaAgrupadorId).Sum(x => x.Valor).ToString();

                objetos.Add("Widget6", graficas.Widget("Total a pagar", widget6, null, null, null));


                return CommandResult.Success(objetos);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
