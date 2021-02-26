using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.InformacionBasicas.Comandos.Eliminar
{
    public class EliminarInformacionBasicaHandler : IRequestHandler<EliminarInformacionBasicaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public EliminarInformacionBasicaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarInformacionBasicaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                InformacionBasica informacionBasica = await this.contexto.InformacionBasicas.FindAsync(request.Id);
                if (informacionBasica == null) return CommandResult.Fail("No existe", 404);

                this.contexto.InformacionBasicas.Remove(informacionBasica);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
