using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de crear la entidad DocumentosFuncionarios
/// </summary>

namespace ApiV3.Dominio.DocumentoFuncionarios.Comandos.Crear
{
    public class CrearDocumentoFuncionarioHandler : IRequestHandler<CrearDocumentoFuncionarioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public CrearDocumentoFuncionarioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearDocumentoFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                DocumentoFuncionario documentoFuncionario = new DocumentoFuncionario
                {
                    FuncionarioId = request.FuncionarioId,
                    TipoSoporteId = request.TipoSoporteId,
                    Comentario = Texto.TipoOracion(request.Comentario),
                    Adjunto = request.Adjunto
                };
                this.contexto.DocumentoFuncionarios.Add(documentoFuncionario);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(documentoFuncionario);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
