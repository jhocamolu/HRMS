using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.DocumentoFuncionarios.Comandos.Parcial
{
    public class ParcialDocumentoFuncionarioHandler : IRequestHandler<ParcialDocumentoFuncionarioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialDocumentoFuncionarioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialDocumentoFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {

                DocumentoFuncionario documentoFuncionario = contexto.DocumentoFuncionarios.Find(request.Id);

                if (request.Activo != null)
                {
                    documentoFuncionario.EstadoRegistro = EstadoRegistro.Activo;
                    if (request.Activo != true)
                    {
                        documentoFuncionario.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                if (request.FuncionarioId != null) documentoFuncionario.FuncionarioId = (int)request.FuncionarioId;
                if (request.TipoSoporteId != null) documentoFuncionario.TipoSoporteId = (int)request.TipoSoporteId;
                if (request.Comentario != null) documentoFuncionario.Comentario = Texto.TipoOracion(request.Comentario);
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
