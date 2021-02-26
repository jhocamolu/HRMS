using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.DocumentoFuncionarios.Comandos.Eliminar
{
    public class EliminarDocumentoFuncionarioHandler : IRequestHandler<EliminarDocumentoFuncionarioRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;
        public EliminarDocumentoFuncionarioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarDocumentoFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                DocumentoFuncionario documentoFuncionario = await this.contexto.DocumentoFuncionarios.FindAsync(request.Id);
                if (documentoFuncionario == null) return CommandResult.Fail("No existe", 404);

                this.contexto.DocumentoFuncionarios.Remove(documentoFuncionario);
                await contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
