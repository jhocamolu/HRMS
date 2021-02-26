using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.HojaDeVidaDocumentos.Comandos.Eliminar
{
    public class EliminarHojaDeVidaDocumentoHandler : IRequestHandler<EliminarHojaDeVidaDocumentoRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;
        public EliminarHojaDeVidaDocumentoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarHojaDeVidaDocumentoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                HojaDeVidaDocumento documento = await this.contexto.HojaDeVidaDocumentos.FindAsync(request.Id);
                if (documento == null) return CommandResult.Fail("No existe", 404);

                this.contexto.HojaDeVidaDocumentos.Remove(documento);
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
