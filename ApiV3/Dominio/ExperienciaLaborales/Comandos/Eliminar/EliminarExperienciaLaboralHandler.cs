using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ExperienciaLaborales.Comandos.Eliminar
{
    public class EliminarExperienciaLaboralHandler : IRequestHandler<EliminarExperienciaLaboralRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarExperienciaLaboralHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarExperienciaLaboralRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ExperienciaLaboral experienciaLaboral = contexto.ExperienciaLaborales.Find(request.Id);
                if (experienciaLaboral == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.ExperienciaLaborales.Remove(experienciaLaboral);
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
