using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.HojaDeVidaEstudios.Comandos.Parcial
{
    public class ParcialHojaDeVidaEstudioHandler : IRequestHandler<ParcialHojaDeVidaEstudioRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;

        public ParcialHojaDeVidaEstudioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialHojaDeVidaEstudioRequest request, CancellationToken cancellationToken)
        {

            try
            {
                var estudio = contexto.HojaDeVidaEstudios.Find(request.Id);

                if (request.Activo != null)
                {
                    estudio.EstadoRegistro = EstadoRegistro.Activo;
                    if (request.Activo != true)
                    {
                        estudio.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }

                contexto.HojaDeVidaEstudios.Update(estudio);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(estudio);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
