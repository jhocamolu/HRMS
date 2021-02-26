using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CentroTrabajos.Comandos.Estado
{
    public class ParcialCentroTrabajoHandler : IRequestHandler<ParcialCentroTrabajoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialCentroTrabajoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(ParcialCentroTrabajoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var centroTrabajo = contexto.CentroTrabajos.Find(request.Id);

                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        centroTrabajo.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        centroTrabajo.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                if (request.Codigo != null)
                {
                    centroTrabajo.Codigo = request.Codigo;
                }
                if (request.Nombre != null)
                {
                    centroTrabajo.Nombre = request.Nombre;
                }
                if (request.PorcentajeRiesgo != 0)
                {
                    centroTrabajo.PorcentajeRiesgo = request.PorcentajeRiesgo;
                }

                contexto.CentroTrabajos.Update(centroTrabajo);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(centroTrabajo);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);

            }
        }
    }
}
