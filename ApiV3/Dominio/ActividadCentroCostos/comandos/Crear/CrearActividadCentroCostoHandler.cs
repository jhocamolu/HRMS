using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ActividadCentroCostos.comandos.Crear
{
    public class CrearActividadCentroCostoHandler : IRequestHandler<CrearActividadCentroCostoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearActividadCentroCostoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearActividadCentroCostoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ActividadCentroCosto actividadCentroCosto = new ActividadCentroCosto();
                actividadCentroCosto.ActividadId = (int)request.ActividadId;
                actividadCentroCosto.CentroCostoId = (int)request.CentroCostoId;
                actividadCentroCosto.MunicipioId = (int)request.MunicipioId;

                this.contexto.ActividadCentroCostos.Add(actividadCentroCosto);
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
