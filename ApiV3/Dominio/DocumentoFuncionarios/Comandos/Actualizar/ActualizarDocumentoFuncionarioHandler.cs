using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de actualziar la entidad DocumentosFuncionarios.
/// </summary>

namespace ApiV3.Dominio.DocumentoFuncionarios.Comandos.Actualizar
{
    public class ActualizarDocumentoFuncionarioHandler : IRequestHandler<ActualizarDocumentoFuncionarioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ActualizarDocumentoFuncionarioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(ActualizarDocumentoFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                DocumentoFuncionario documentoFuncionario = this.contexto.DocumentoFuncionarios.Find(request.Id);

                documentoFuncionario.FuncionarioId = request.FuncionarioId;
                documentoFuncionario.TipoSoporteId = request.TipoSoporteId;
                documentoFuncionario.Comentario = Texto.TipoOracion(request.Comentario);
                if (request.Adjunto != null) documentoFuncionario.Adjunto = request.Adjunto;

                contexto.DocumentoFuncionarios.Update(documentoFuncionario);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(documentoFuncionario);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
