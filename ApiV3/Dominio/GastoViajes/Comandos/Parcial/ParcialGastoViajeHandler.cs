using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.GastoViajes.Comandos.Parcial
{
    public class ParcialGastoViajeHandler : IRequestHandler<ParcialGastoViajeRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialGastoViajeHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialGastoViajeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                GastoViaje gastoViaje = contexto.GastoViajes.Find(request.Id);
                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        gastoViaje.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        gastoViaje.EstadoRegistro = EstadoRegistro.Inactivo;
                    }

                }

                contexto.GastoViajes.Update(gastoViaje);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(gastoViaje);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
