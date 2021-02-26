using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using ApiV3.Servicios.Peticion;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Notificaciones.Comandos.Ejecutar
{
    public class EjecutarNotificacionHandler : IRequestHandler<EjecutarNotificacionRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IPeticionService peticionService;
        private readonly IConfiguration configuration;

        public EjecutarNotificacionHandler(NominaDbContext contexto, IPeticionService peticionService, IConfiguration configuration)
        {
            this.contexto = contexto;
            this.peticionService = peticionService;
            this.configuration = configuration;
        }

        public async Task<CommandResult> Handle(EjecutarNotificacionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Notificacion notificacion = contexto.Notificaciones.FirstOrDefault(x => x.Id == request.Id);
                if (notificacion == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                if (notificacion.EnEjecucion)
                {
                    return CommandResult.Fail("Notificacion en ejecucion.", 400);
                }

                string instruccion;
                if (notificacion.Tipo == TipoNotificacion.Email)
                {
                    TareaProgramada tareasEmail = contexto.TareaProgramadas.FirstOrDefault(x => x.Alias == "notificacion-email");
                    if (tareasEmail == null)
                    {
                        return CommandResult.Fail("No existe la tarea programada con alias 'notificacion-email'.", 400);
                    }
                    instruccion = tareasEmail.Instruccion;
                }
                else
                {
                    TareaProgramada tareasPush = contexto.TareaProgramadas.FirstOrDefault(x => x.Alias == "notificacion-push");
                    if (tareasPush == null)
                    {
                        return CommandResult.Fail("No existe la tarea programada con alias 'notificacion-push'.", 400);
                    }
                    instruccion = tareasPush.Instruccion;
                }


                ProcessStartInfo startInfo = new ProcessStartInfo("powershell.exe")
                {
                    WindowStyle = ProcessWindowStyle.Minimized,
                    Arguments = instruccion + " " + request.Id
                };
                Process.Start(startInfo);

                return CommandResult.Success();

            }
            catch (System.Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
