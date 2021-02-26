using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.SolicitudPermisos.Comandos.Estado
{
    public class EstadoSolicitudPermisoHandler : IRequestHandler<EstadoSolicitudPermisoRequest, CommandResult>
    {
        private NominaDbContext contexto;

        public EstadoSolicitudPermisoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EstadoSolicitudPermisoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                SolicitudPermiso solicitudPermiso = contexto.SolicitudPermisos.Find(request.Id);
                if (request.Estado != null)
                {
                    if (request.Estado == EstadoSolicitudPermiso.Autorizada)
                    {
                        solicitudPermiso.Estado = EstadoSolicitudPermiso.Autorizada;
                    }
                    if (request.Estado == EstadoSolicitudPermiso.Aprobada)
                    {
                        solicitudPermiso.Estado = EstadoSolicitudPermiso.Aprobada;
                    }
                    if (request.Estado == EstadoSolicitudPermiso.Rechazada)
                    {
                        solicitudPermiso.Estado = EstadoSolicitudPermiso.Rechazada;
                    }
                    if (request.Estado == EstadoSolicitudPermiso.Cancelada)
                    {
                        solicitudPermiso.Estado = EstadoSolicitudPermiso.Cancelada;
                    }
                }
                if (request.Justificacion != null)
                {
                    solicitudPermiso.Justificacion = request.Justificacion;
                }

                contexto.SolicitudPermisos.Update(solicitudPermiso);
                await contexto.SaveChangesAsync();


                //Si la solicitud cambia a estado Autorizada guarda el registro en tabla AusentismoFuncionario
                if (solicitudPermiso.Estado == EstadoSolicitudPermiso.Autorizada)
                {
                    AusentismoFuncionario ausentismoFuncionario = new AusentismoFuncionario();
                    ausentismoFuncionario.FuncionarioId = (int)solicitudPermiso.FuncionarioId;
                    ausentismoFuncionario.TipoAusentismoId = (int)solicitudPermiso.TipoAusentismoId;
                    ausentismoFuncionario.FechaInicio = (DateTime)solicitudPermiso.FechaInicio;
                    ausentismoFuncionario.FechaFin = (DateTime)solicitudPermiso.FechaFin;
                    ausentismoFuncionario.HoraInicio = solicitudPermiso.HoraSalida;
                    ausentismoFuncionario.HoraFin = solicitudPermiso.HoraLlegada;
                    ausentismoFuncionario.Estado = EstadoAusentismo.Aprobado;
                    //Cargar soporte 
                    var soporte = contexto.SoporteSolicitudPermisos.FirstOrDefault(x => x.SolicitudPermisoId == solicitudPermiso.Id);
                    if (soporte != null)
                    {
                        ausentismoFuncionario.Adjunto = soporte.Adjunto;
                    }
                    contexto.AusentismoFuncionarios.Add(ausentismoFuncionario);
                    await contexto.SaveChangesAsync();
                }
                return CommandResult.Success(solicitudPermiso);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
