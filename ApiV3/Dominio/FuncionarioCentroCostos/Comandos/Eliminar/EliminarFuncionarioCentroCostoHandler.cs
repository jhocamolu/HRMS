using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.FuncionarioCentroCostos.Comandos.Eliminar
{
    public class EliminarFuncionarioCentroCostoHandler : IRequestHandler<EliminarFuncionarioCentroCostoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarFuncionarioCentroCostoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarFuncionarioCentroCostoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var funcionarioCentro = contexto.FuncionarioCentroCostos
                                            .Where(x => x.FuncionarioId == request.FuncionarioId
                                                    && x.Estado == EstadoFuncionarioCentroCosto.Pendiente
                                                    && x.FormaRegistro == FormaRegistroFuncionarioCentroCosto.Automatico);

                if (funcionarioCentro != null)
                {
                    contexto.FuncionarioCentroCostos.RemoveRange(funcionarioCentro);
                    await contexto.SaveChangesAsync();
                }
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
