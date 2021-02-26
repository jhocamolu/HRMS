using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CargoGrados.Crear
{
    public class CrearCargoGradoHandler : IRequestHandler<CrearCargoGradoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearCargoGradoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearCargoGradoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                CargoGrado cargoGrado = new CargoGrado
                {
                    Nombre = request.Nombre,
                    Descripcion = request.Descripcion,
                    CargoId = (int)request.CargoId
                };
                contexto.CargoGrados.Add(cargoGrado);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(cargoGrado);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
