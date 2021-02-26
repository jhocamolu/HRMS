using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CargoGrados.Parcial
{
    public class ParcialCargoGradoHandler : IRequestHandler<ParcialCargoGradoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialCargoGradoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialCargoGradoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                CargoGrado cargoGrado = this.contexto.CargoGrados.Find(request.Id);
                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        cargoGrado.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        cargoGrado.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }

                if (request.Nombre != null)
                {
                    cargoGrado.Nombre = request.Nombre;
                }
                if (request.Descripcion != null)
                {
                    cargoGrado.Descripcion = request.Descripcion;
                }
                if (request.CargoId != null)
                {
                    cargoGrado.CargoId = (int)request.CargoId;
                }

                contexto.CargoGrados.Update(cargoGrado);
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
