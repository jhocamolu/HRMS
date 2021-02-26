using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Actividades.comandos.Actualizar
{
    public class ActualizarActividadHandler : IRequestHandler<ActualizarActividadRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarActividadHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarActividadRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Actividad actividad = contexto.Actividades.Find(request.Id);
                actividad.Codigo = request.Codigo.ToUpper();
                actividad.Nombre = request.Nombre;
                actividad.PromedioProductividad = (int)request.PromedioProductividad;
                actividad.ValorComplejidad = ((decimal)1 / (decimal)request.PromedioProductividad);
                actividad.Descripcion = request.Descripcion;
                this.contexto.Actividades.Update(actividad);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(actividad);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
