using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.HojaDeVidaExperienciaLaborales.Comandos.Eliminar
{
    public class EliminarHojaDeVidaExperienciaLaboralHandler : IRequestHandler<EliminarHojaDeVidaExperienciaLaboralRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarHojaDeVidaExperienciaLaboralHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarHojaDeVidaExperienciaLaboralRequest request, CancellationToken cancellationToken)
        {
            try
            {
                HojaDeVidaExperienciaLaboral hojaVidaExperiencia = contexto.HojaDeVidaExperienciaLaborales.Find(request.Id);
                if (hojaVidaExperiencia == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.HojaDeVidaExperienciaLaborales.Remove(hojaVidaExperiencia);
                await contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
