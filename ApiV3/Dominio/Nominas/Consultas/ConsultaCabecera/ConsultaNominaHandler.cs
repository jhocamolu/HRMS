using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Nominas.Consultas.ConsultaCabecera
{
    public class ConsultaNominaHandler : IRequestHandler<ConsultaNominaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ConsultaNominaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ConsultaNominaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var nominaFuncionario = contexto.NominaFuncionarios.Where(x => x.NominaId == request.NominaId &&
                                                                    (x.Estado == EstadoNominaFuncionario.EnLiquidacion ||
                                                                    x.Estado == EstadoNominaFuncionario.Pendiente))
                                .ToList();
                Nomina nomina = await this.contexto.Nominas.Include(x => x.PeriodoContable).Include(x => x.SubPeriodo).Include(x => x.TipoLiquidacion).FirstOrDefaultAsync(x => x.Id == request.NominaId);

                if (nominaFuncionario.Any())
                {
                    nomina.Estado = EstadoNomina.EnLiquidacion;
                }
                return CommandResult.Success(nomina);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
