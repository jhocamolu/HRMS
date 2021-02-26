using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.InformacionFamiliares.Comandos.Eliminar
{
    public class EliminarInformacionFamiliarHandler : IRequestHandler<EliminarInformacionFamiliarRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarInformacionFamiliarHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarInformacionFamiliarRequest request, CancellationToken cancellationToken)
        {
            try
            {
                InformacionFamiliar informacionFamiliar = await this.contexto.InformacionFamiliares.FindAsync(request.Id);
                if (informacionFamiliar == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.InformacionFamiliares.Remove(informacionFamiliar);
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
