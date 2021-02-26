using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.NivelCargos.Comandos.Crear
{
    public class CrearNivelCargoHandler : IRequestHandler<CrearNivelCargoRequest, CommandResult>
    {
        private NominaDbContext contexto;

        public CrearNivelCargoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearNivelCargoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                NivelCargo nivelCargo = new NivelCargo
                {
                    Nombre = request.Nombre.ToUpper()
                };
                this.contexto.NivelCargos.Add(nivelCargo);
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
