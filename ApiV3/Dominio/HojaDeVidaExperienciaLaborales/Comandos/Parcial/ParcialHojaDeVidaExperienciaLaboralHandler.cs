using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.HojaDeVidaExperienciaLaborales.Comandos.Parcial
{
    public class ParcialHojaDeVidaExperienciaLaboralHandler : IRequestHandler<ParcialHojaDeVidaExperienciaLaboralRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialHojaDeVidaExperienciaLaboralHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialHojaDeVidaExperienciaLaboralRequest request, CancellationToken cancellationToken)
        {
            try
            {
                HojaDeVidaExperienciaLaboral experiencia = contexto.HojaDeVidaExperienciaLaborales.Find(request.Id);

                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        experiencia.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        experiencia.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                contexto.HojaDeVidaExperienciaLaborales.Update(experiencia);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(experiencia);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
