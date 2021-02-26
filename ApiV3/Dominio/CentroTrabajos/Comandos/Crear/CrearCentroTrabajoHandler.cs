using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CentroTrabajos.Comandos.Crear
{
    public class CrearCentroTrabajoHandler : IRequestHandler<CrearCentroTrabajoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearCentroTrabajoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearCentroTrabajoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                CentroTrabajo centroTrabajo = new CentroTrabajo
                {
                    Codigo = request.Codigo,
                    PorcentajeRiesgo = request.PorcentajeRiesgo,
                    Nombre = request.Nombre
                };
                this.contexto.CentroTrabajos.Add(centroTrabajo);
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
