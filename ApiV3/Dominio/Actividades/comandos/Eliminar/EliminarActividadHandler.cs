using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Actividades.comandos.Eliminar
{
    public class EliminarActividadHandler : IRequestHandler<EliminarActividadRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarActividadHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarActividadRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Actividad actividad = contexto.Actividades.Find(request.Id);
                if (actividad == null)
                {
                    CommandResult.Fail("No existe", 404);
                }
                this.contexto.Actividades.Remove(actividad);
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
