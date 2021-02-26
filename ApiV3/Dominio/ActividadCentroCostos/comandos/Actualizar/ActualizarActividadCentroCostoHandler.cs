using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ActividadCentroCostos.comandos.Actualizar
{
    public class ActualizarActividadCentroCostoHandler : IRequestHandler<ActualizarActividadCentroCostoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarActividadCentroCostoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarActividadCentroCostoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ActividadCentroCosto actividadCentroCosto = this.contexto.ActividadCentroCostos.Find(request.Id);
                actividadCentroCosto.ActividadId = (int)request.ActividadId;
                actividadCentroCosto.CentroCostoId = (int)request.CentroCostoId;
                actividadCentroCosto.MunicipioId = (int)request.MunicipioId;

                this.contexto.ActividadCentroCostos.Update(actividadCentroCosto);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(actividadCentroCosto);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
