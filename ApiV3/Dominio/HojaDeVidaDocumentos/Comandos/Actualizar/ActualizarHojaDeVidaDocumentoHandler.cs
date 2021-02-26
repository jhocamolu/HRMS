using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.HojaDeVidaDocumentos.Comandos.Actualizar
{
    public class ActualizarHojaDeVidaDocumentoHandler : IRequestHandler<ActualizarHojaDeVidaDocumentoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ActualizarHojaDeVidaDocumentoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarHojaDeVidaDocumentoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                HojaDeVidaDocumento documento = await this.contexto.HojaDeVidaDocumentos.FindAsync(request.Id);

                documento.HojaDeVidaId = (int)request.HojaDeVidaId;
                documento.TipoSoporteId = (int)request.TipoSoporteId;
                documento.Comentario = Texto.TipoOracion(request.Comentario);
                if (request.Adjunto != null) documento.Adjunto = request.Adjunto;

                contexto.HojaDeVidaDocumentos.Update(documento);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(documento);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
