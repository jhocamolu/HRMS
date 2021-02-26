using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CargoGrupos.Comandos.Crear
{
    public class CrearCargoGrupoHandler : IRequestHandler<CrearCargoGrupoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearCargoGrupoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearCargoGrupoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                CargoGrupo cargoGrupo = new CargoGrupo();
                cargoGrupo.CargoId = (int)request.CargoId;
                cargoGrupo.GrupoId = (int)request.GrupoId;
                contexto.CargoGrupos.Add(cargoGrupo);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(cargoGrupo);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
