using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.HojaDeVidaDocumentos.Comandos.Crear
{
    public class CrearHojaDeVidaDocumentoHandler : IRequestHandler<CrearHojaDeVidaDocumentoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public CrearHojaDeVidaDocumentoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearHojaDeVidaDocumentoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                HojaDeVidaDocumento documento = new HojaDeVidaDocumento
                {
                    HojaDeVidaId = (int)request.HojaDeVidaId,
                    TipoSoporteId = (int)request.TipoSoporteId,
                    Comentario = Texto.TipoOracion(request.Comentario),
                    Adjunto = request.Adjunto
                };
                this.contexto.HojaDeVidaDocumentos.Add(documento);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(documento);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
