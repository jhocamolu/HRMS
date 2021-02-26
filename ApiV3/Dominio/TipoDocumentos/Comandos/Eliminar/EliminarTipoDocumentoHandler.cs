using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de Eliminar el tipoDocumento.
/// </summary>

namespace ApiV3.Dominio.TipoDocumentos.Comandos.Eliminar
{
    public class EliminarTipoDocumentoHandler : IRequestHandler<EliminarTipoDocumentoRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;
        public EliminarTipoDocumentoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }


        public async Task<CommandResult> Handle(EliminarTipoDocumentoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoDocumento tipoDocumento = await this.contexto.TipoDocumentos.FindAsync(request.Id);
                if (tipoDocumento == null) return CommandResult.Fail("No existe", 404);

                this.contexto.TipoDocumentos.Remove(tipoDocumento);
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
