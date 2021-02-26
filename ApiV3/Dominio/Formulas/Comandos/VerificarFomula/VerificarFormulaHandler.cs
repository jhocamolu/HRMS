using ApiV3.Infraestructura.Resultados;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Formulas.Comandos.VerificarFomula
{
    public class VerificarFormulaHandler : IRequestHandler<VerificarFormulaRequest, CommandResult>
    {

        public async Task<CommandResult> Handle(VerificarFormulaRequest request, CancellationToken cancellationToken)
        {
            return CommandResult.Success();
        }
    }
}
