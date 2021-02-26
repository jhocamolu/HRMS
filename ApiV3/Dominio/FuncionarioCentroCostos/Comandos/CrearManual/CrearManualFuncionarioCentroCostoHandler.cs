using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.FuncionarioCentroCostos.Comandos.CrearManual
{
    public class CrearManualFuncionarioCentroCostoHandler : IRequestHandler<CrearManualFuncionarioCentroCostoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearManualFuncionarioCentroCostoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearManualFuncionarioCentroCostoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                dynamic resultado = null;
                if (request.TipoDistribucion == TipoDsitribucionFuncionarioCentroCosto.Funcionario)
                {
                    foreach (var item in request.ListaFucnionariosCentroCosto)
                    {
                        int ActividadCentroCostosId = contexto.ActividadCentroCostos
                                            .FirstOrDefault(x => x.CentroCostoId == item.CentroCostoId).Id;
                        var funcionarioCentroCosto = new FuncionarioCentroCosto
                        {
                            FuncionarioId = (int)request.FuncionarioId,
                            FormaRegistro = FormaRegistroFuncionarioCentroCosto.Manual,
                            Estado = EstadoFuncionarioCentroCosto.Pendiente,
                            FechaCorte = request.FechaCorte,
                            ActividadCentroCostoId = ActividadCentroCostosId
                        };
                        funcionarioCentroCosto.Porcentaje = item.Porcentaje / 100;

                        contexto.FuncionarioCentroCostos.Add(funcionarioCentroCosto);
                        await contexto.SaveChangesAsync();
                        resultado = funcionarioCentroCosto;
                    }
                }
                else
                {
                    foreach (var item in request.ListaFucnionariosCentroCosto)
                    {
                        int ActividadCentroCostosId = contexto.ActividadCentroCostos
                                            .FirstOrDefault(x => x.CentroCostoId == item.CentroCostoId).Id;
                        var cargoCentroCosto = new CargoCentroCosto
                        {
                            CargoId = (int)request.CargoId,
                            FechaCorte = request.FechaCorte,
                            ActividadCentroCostoId = ActividadCentroCostosId,
                            CentroOperativoId = (int)request.CentroOperativoId
                        };
                        double por = item.Porcentaje / 100;
                        cargoCentroCosto.Porcentaje = item.Porcentaje / 100;

                        contexto.CargoCentroCostos.Add(cargoCentroCosto);
                        await contexto.SaveChangesAsync();
                        resultado = cargoCentroCosto;
                    }
                }
                return CommandResult.Success(resultado);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
