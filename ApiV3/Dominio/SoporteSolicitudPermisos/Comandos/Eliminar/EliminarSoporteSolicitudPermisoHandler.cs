using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.SoporteSolicitudPermisos.Comandos.Eliminar
{
    public class EliminarSoporteSolicitudPermisoHandler : IRequestHandler<EliminarSoporteSolicitudPermisoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarSoporteSolicitudPermisoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarSoporteSolicitudPermisoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                SoporteSolicitudPermiso soporteSolicitudPermiso = await this.contexto.SoporteSolicitudPermisos.FindAsync(request.Id);
                if (soporteSolicitudPermiso == null) return CommandResult.Fail("No existe", 404);

                this.contexto.SoporteSolicitudPermisos.Remove(soporteSolicitudPermiso);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
