using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ActividadCentroCostos.comandos.Parcial
{
    public class ParcialActividadCentroCostoHandler : IRequestHandler<ParcialActividadCentroCostoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialActividadCentroCostoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialActividadCentroCostoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ActividadCentroCosto actividadCentroCosto = this.contexto.ActividadCentroCostos.Find(request.Id);
                if (request.ActividadId != null)
                {
                    actividadCentroCosto.ActividadId = (int)request.ActividadId;
                }
                if (request.CentroCostoId != null)
                {
                    actividadCentroCosto.CentroCostoId = (int)request.CentroCostoId;
                }
                if (request.MunicipioId != null)
                {
                    actividadCentroCosto.MunicipioId = (int)request.MunicipioId;
                }
                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        actividadCentroCosto.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    if (request.Activo == false)
                    {
                        actividadCentroCosto.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }

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
