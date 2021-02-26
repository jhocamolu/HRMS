using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de eliminar registros de la entidad DiagnosticoCie
/// </summary>

namespace ApiV3.Dominio.DiagnosticoCies.Comandos.Eliminar
{
    public class EliminarDiagnosticoCieHandler : IRequestHandler<EliminarDiagnosticoCieRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public EliminarDiagnosticoCieHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarDiagnosticoCieRequest request, CancellationToken cancellationToken)
        {
            try
            {
                DiagnosticoCie diagnosticoCie = await this.contexto.DiagnosticoCies.FindAsync(request.Id);
                if (diagnosticoCie == null) return CommandResult.Fail("No existe", 404);

                this.contexto.DiagnosticoCies.Remove(diagnosticoCie);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
