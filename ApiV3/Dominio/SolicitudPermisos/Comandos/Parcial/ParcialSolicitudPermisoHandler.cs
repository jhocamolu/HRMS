using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.SolicitudPermisos.Comandos.Parcial
{
    public class ParcialSolicitudPermisoHandler : IRequestHandler<ParcialSolicitudPermisoRequest, CommandResult>
    {
        private NominaDbContext contexto;

        public ParcialSolicitudPermisoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialSolicitudPermisoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                SolicitudPermiso solicitudPermiso = contexto.SolicitudPermisos.Find(request.Id);
                if (request.FuncionarioId != null)
                {
                    solicitudPermiso.FuncionarioId = (int)request.FuncionarioId;
                }
                if (request.TipoAusentismoId != null)
                {
                    solicitudPermiso.TipoAusentismoId = (int)request.TipoAusentismoId;
                }
                if (request.FechaInicio != null)
                {
                    solicitudPermiso.FechaInicio = (DateTime)request.FechaInicio;
                }
                if (request.FechaFin != null)
                {
                    solicitudPermiso.FechaFin = (DateTime)request.FechaFin;
                }
                if (request.HoraLlegada != null)
                {
                    solicitudPermiso.HoraLlegada = (TimeSpan)request.HoraLlegada;
                }
                if (request.HoraSalida != null)
                {
                    solicitudPermiso.HoraSalida = (TimeSpan)request.HoraSalida;
                }
                if (request.Observaciones != null)
                {
                    solicitudPermiso.Observaciones = request.Observaciones;
                }

                contexto.SolicitudPermisos.Update(solicitudPermiso);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(solicitudPermiso);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);

            }
        }
    }
}
