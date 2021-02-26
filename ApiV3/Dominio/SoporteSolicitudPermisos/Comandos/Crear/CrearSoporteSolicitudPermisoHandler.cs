using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.SoporteSolicitudPermisos.Comandos.Crear
{
    public class CrearSoporteSolicitudPermisoHandler : IRequestHandler<CrearSoporteSolicitudPermisoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearSoporteSolicitudPermisoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearSoporteSolicitudPermisoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                SoporteSolicitudPermiso soporteSolicitudPermiso = new SoporteSolicitudPermiso();
                soporteSolicitudPermiso.SolicitudPermisoId = (int)request.SolicitudPermisoId;
                soporteSolicitudPermiso.TipoSoporteId = (int)request.TipoSoporteId;
                soporteSolicitudPermiso.Adjunto = request.Adjunto;
                soporteSolicitudPermiso.Comentario = request.Comentario;

                contexto.SoporteSolicitudPermisos.Add(soporteSolicitudPermiso);
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
