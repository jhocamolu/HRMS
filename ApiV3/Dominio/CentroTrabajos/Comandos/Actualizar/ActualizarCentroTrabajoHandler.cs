using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CentroTrabajos.Comandos.Actualizar
{
    public class ActualizarCentroTrabajoHandler : IRequestHandler<ActualizarCentroTrabajoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarCentroTrabajoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarCentroTrabajoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                CentroTrabajo centroTrabajo = this.contexto.CentroTrabajos.Find(request.Id);

                centroTrabajo.Codigo = request.Codigo;
                centroTrabajo.Nombre = request.Nombre;
                centroTrabajo.PorcentajeRiesgo = request.PorcentajeRiesgo;

                this.contexto.CentroTrabajos.Update(centroTrabajo);
                await this.contexto.SaveChangesAsync();

                return CommandResult.Success(centroTrabajo);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
