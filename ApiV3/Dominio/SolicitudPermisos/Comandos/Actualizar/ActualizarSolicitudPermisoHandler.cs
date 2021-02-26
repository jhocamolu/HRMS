using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.SolicitudPermisos.Comandos.Actualizar
{
    public class ActualizarSolicitudPermisoHandler : IRequestHandler<ActualizarSolicitudPermisoRequest, CommandResult>
    {
        private NominaDbContext contexto;

        public ActualizarSolicitudPermisoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarSolicitudPermisoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                SolicitudPermiso solicitudPermiso = contexto.SolicitudPermisos.Find(request.Id);
                solicitudPermiso.FuncionarioId = (int)request.FuncionarioId;
                solicitudPermiso.TipoAusentismoId = (int)request.TipoAusentismoId;
                solicitudPermiso.FechaInicio = (DateTime)request.FechaInicio;
                solicitudPermiso.FechaFin = (DateTime)request.FechaFin;
                if (request.HoraLlegada != null)
                {
                    solicitudPermiso.HoraLlegada = (TimeSpan)request.HoraLlegada;
                }
                if (request.HoraSalida != null)
                {
                    solicitudPermiso.HoraSalida = (TimeSpan)request.HoraSalida;

                }
                solicitudPermiso.Observaciones = request.Observaciones;

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
