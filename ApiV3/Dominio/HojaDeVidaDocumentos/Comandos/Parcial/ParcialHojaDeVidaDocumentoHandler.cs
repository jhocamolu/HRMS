using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.HojaDeVidaDocumentos.Comandos.Parcial
{

    public class ParcialHojaDeVidaDocumentoHandler : IRequestHandler<ParcialHojaDeVidaDocumentoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialHojaDeVidaDocumentoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialHojaDeVidaDocumentoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                HojaDeVidaDocumento documento = contexto.HojaDeVidaDocumentos.Find(request.Id);

                if (request.Activo != null)
                {
                    documento.EstadoRegistro = EstadoRegistro.Activo;
                    if (request.Activo != true)
                    {
                        documento.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
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
