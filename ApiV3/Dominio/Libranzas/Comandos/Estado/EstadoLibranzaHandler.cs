using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Libranzas.Comandos.Estado
{
    public class EstadoLibranzaHandler : IRequestHandler<EstadoLibranzaRequest, CommandResult>
    {
        private NominaDbContext contexto;

        public EstadoLibranzaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EstadoLibranzaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Libranza libranza = contexto.Libranzas.Find(request.Id);
                if (request.Estado != null)
                {
                    if (request.Estado == EstadoLibranza.Anulada)
                    {
                        libranza.Estado = EstadoLibranza.Anulada;
                    }
                    if (request.Estado == EstadoLibranza.Terminada)
                    {
                        libranza.Estado = EstadoLibranza.Terminada;
                    }
                }
                libranza.Justificacion = request.Justificacion;
                contexto.Libranzas.Update(libranza);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(libranza);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
