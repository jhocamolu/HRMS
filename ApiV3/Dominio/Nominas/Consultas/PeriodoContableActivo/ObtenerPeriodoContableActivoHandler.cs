using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Nominas.Consultas.PeriodoContableActivo
{
    public class ObtenerPeriodoContableActivoHandler : IRequestHandler<ObtenerPeriodoContableActivoRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;

        public ObtenerPeriodoContableActivoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ObtenerPeriodoContableActivoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = this.contexto.PeriodoContables.Where(x => x.Estado == EstadoPeriodoContable.Activo && x.EstadoRegistro == EstadoRegistro.Activo)
                                                            .ToList();
                dynamic consultaPeriodoContable = "";
                if (result.Count > 1)
                {
                    return CommandResult.Fail("Existe más de un período contable activo", 404);
                }
                if (result.Count == 0)
                {
                    return CommandResult.Fail("No se encuentra período contable activo", 404);
                }
                if (result.Count == 1)
                {
                    foreach (var item in result)
                    {
                        consultaPeriodoContable = await this.contexto.PeriodoContables.FindAsync(item.Id);
                    }
                }
                return CommandResult.Success(consultaPeriodoContable);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message, 500);
            }
        }
    }
}