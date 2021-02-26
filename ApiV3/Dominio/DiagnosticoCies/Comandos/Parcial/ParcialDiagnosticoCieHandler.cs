using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de realizar las actualizaciones parciales a la entidad DiagnostiCie
/// Se valida que campos tienen valor, es decir diferentes a null, para actualizar esos campos
/// </summary>

namespace ApiV3.Dominio.DiagnosticoCies.Comandos.Parcial
{
    public class ParcialDiagnosticoCieHandler : IRequestHandler<ParcialDiagnosticoCieRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;
        public ParcialDiagnosticoCieHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialDiagnosticoCieRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var diagnosticoCie = this.contexto.DiagnosticoCies.Find(request.Id);

                if (request.Codigo != null) diagnosticoCie.Codigo = request.Codigo.ToUpper();
                if (request.Nombre != null) diagnosticoCie.Nombre = Texto.TipoOracion(request.Nombre);
                if (request.Activo != null)
                {
                    diagnosticoCie.EstadoRegistro = EstadoRegistro.Activo;
                    if (request.Activo != true) diagnosticoCie.EstadoRegistro = EstadoRegistro.Inactivo;
                }

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
