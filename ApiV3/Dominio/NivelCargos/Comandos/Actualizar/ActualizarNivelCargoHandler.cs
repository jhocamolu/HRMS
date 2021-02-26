using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.NivelCargos.Comandos.Actualizar
{
    public class ActualizarNivelCargoHandler : IRequestHandler<ActualizarNivelCargoRequest, CommandResult>
    {
        private NominaDbContext contexto;

        public ActualizarNivelCargoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarNivelCargoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                NivelCargo nivelCargo = this.contexto.NivelCargos.Find(request.Id);
                nivelCargo.Nombre = request.Nombre.ToUpper();

                this.contexto.NivelCargos.Update(nivelCargo);
                await this.contexto.SaveChangesAsync();

                return CommandResult.Success(nivelCargo);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
