using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Contratos.Comandos.Finalizar
{
    public class FinalizarContratoHandler : IRequestHandler<FinalizarContratoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public FinalizarContratoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(FinalizarContratoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                DateTime hoy = DateTime.Now;
                Contrato contrato = await this.contexto.Contratos.FindAsync(request.Id);
                contrato.CausalTerminacionId = request.CausalTerminacionId;
                contrato.ObservacionFinalizacionContrato = request.ObservacionFinalizacionContrato;
                contrato.FechaTerminacion = request.FechaTerminacion;
                if (request.FechaTerminacion >= hoy.AddYears(-100) && request.FechaTerminacion <= hoy)
                {
                    contrato.Estado = EstadoContrato.PendientePorLiquidar;

                    Funcionario funcionario = this.contexto.Funcionarios.FirstOrDefault(x => x.Id == contrato.FuncionarioId);
                    if (funcionario.Estado == EstadoFuncionario.Activo || funcionario.Estado == EstadoFuncionario.EnVacaciones)
                    {
                        funcionario.Estado = EstadoFuncionario.Retirado;
                    }
                    contexto.Funcionarios.Update(funcionario);
                    await contexto.SaveChangesAsync();

                }

                contexto.Contratos.Update(contrato);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(contrato);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
