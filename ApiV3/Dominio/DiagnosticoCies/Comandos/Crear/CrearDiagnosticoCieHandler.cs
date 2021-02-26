using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.DiagnosticoCies.Comandos.Crear
{
    public class CrearDiagnosticoCieHandler : IRequestHandler<CrearDiagnosticoCieRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;
        public CrearDiagnosticoCieHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearDiagnosticoCieRequest request, CancellationToken cancellationToken)
        {
            try
            {
                DiagnosticoCie diagnosticoCie = new DiagnosticoCie
                {

                    Codigo = request.Codigo.ToUpper(),
                    Nombre = Texto.TipoOracion(request.Nombre)
                };

                this.contexto.DiagnosticoCies.Add(diagnosticoCie);
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
