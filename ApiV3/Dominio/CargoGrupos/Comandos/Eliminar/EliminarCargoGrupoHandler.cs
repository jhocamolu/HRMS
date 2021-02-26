using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CargoGrupos.Comandos.Eliminar
{
    public class EliminarCargoGrupoHandler : IRequestHandler<EliminarCargoGrupoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarCargoGrupoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarCargoGrupoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Valida si el cargo esta asociado a un contrato o un otro si
                CargoGrupo cargoGrupo = await this.contexto.CargoGrupos.FindAsync(request.Id);
                if (cargoGrupo.Defecto != true)
                {
                    if (cargoGrupo == null)
                    {
                        return CommandResult.Fail("No existe", 404);
                    }

                    this.contexto.CargoGrupos.Remove(cargoGrupo);
                    await contexto.SaveChangesAsync();
                }

                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
