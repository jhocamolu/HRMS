using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.SoporteSolicitudPermisos.Comandos.Actualizar
{
    public class ActualizarSoporteSolicitudPermisoHandler : IRequestHandler<ActualizarSoporteSolicitudPermisoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarSoporteSolicitudPermisoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarSoporteSolicitudPermisoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                SoporteSolicitudPermiso soporteSolicitudPermiso = contexto.SoporteSolicitudPermisos.Find(request.Id);
                if (request.SolicitudPermisoId != null)
                {
                    soporteSolicitudPermiso.SolicitudPermisoId = (int)request.SolicitudPermisoId;
                }
                if (request.TipoSoporteId != null)
                {
                    soporteSolicitudPermiso.TipoSoporteId = (int)request.TipoSoporteId;
                }
                soporteSolicitudPermiso.Adjunto = request.Adjunto;
                soporteSolicitudPermiso.Comentario = request.Comentario;

                contexto.SoporteSolicitudPermisos.Update(soporteSolicitudPermiso);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(soporteSolicitudPermiso);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
