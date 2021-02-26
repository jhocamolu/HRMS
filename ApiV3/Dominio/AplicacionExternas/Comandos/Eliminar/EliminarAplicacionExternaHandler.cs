using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.AplicacionExternas.Comandos.Eliminar
{
    public class EliminarAplicacionExternaHandler : IRequestHandler<EliminarAplicacionExternaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarAplicacionExternaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarAplicacionExternaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                AplicacionExterna aplicacionExterna = contexto.AplicacionExternas.Find(request.Id);
                if (aplicacionExterna == null)
                {
                    CommandResult.Fail("No existe", 404);
                }
                this.contexto.AplicacionExternas.Remove(aplicacionExterna);
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
