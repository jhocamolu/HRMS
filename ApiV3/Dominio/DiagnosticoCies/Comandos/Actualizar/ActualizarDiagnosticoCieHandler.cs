using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de realizar la actualizacion a la entidad DiagnosticoCIe
/// </summary>

namespace ApiV3.Dominio.DiagnosticoCies.Comandos.Actualizar
{
    public class ActualizarDiagnosticoCieHandler : IRequestHandler<ActualizarDiagnosticoCieRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;
        public ActualizarDiagnosticoCieHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarDiagnosticoCieRequest request, CancellationToken cancellationToken)
        {
            try
            {
                DiagnosticoCie diagnosticoCie = this.contexto.DiagnosticoCies.Find(request.Id);

                diagnosticoCie.Codigo = request.Codigo.ToUpper();
                diagnosticoCie.Nombre = Texto.TipoOracion(request.Nombre);

                this.contexto.DiagnosticoCies.Update(diagnosticoCie);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(diagnosticoCie);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
