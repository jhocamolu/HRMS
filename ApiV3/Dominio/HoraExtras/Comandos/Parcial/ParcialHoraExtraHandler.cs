using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.HoraExtras.Comandos.Parcial
{
    public class ParcialHoraExtraHandler : IRequestHandler<ParcialHoraExtraRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialHoraExtraHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialHoraExtraRequest request, CancellationToken cancellationToken)
        {
            try
            {
                HoraExtra horaExtra = contexto.HoraExtras.Find(request.Id);
                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        horaExtra.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        horaExtra.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                contexto.HoraExtras.Update(horaExtra);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(horaExtra);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
