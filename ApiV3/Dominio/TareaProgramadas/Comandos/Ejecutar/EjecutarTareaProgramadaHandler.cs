using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Servicios.Peticion;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TareaProgramadas.Comandos.Ejecutar
{
    public class EjecutarTareaProgramadaHandler : IRequestHandler<EjecutarTareaProgramadaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IPeticionService peticionService;
        private readonly IConfiguration configuration;

        public EjecutarTareaProgramadaHandler(NominaDbContext contexto, IPeticionService peticionService, IConfiguration configuration)
        {
            this.contexto = contexto;
            this.peticionService = peticionService;
            this.configuration = configuration;
        }

        async Task<CommandResult> IRequestHandler<EjecutarTareaProgramadaRequest, CommandResult>.Handle(EjecutarTareaProgramadaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var tareaProgramada = this.contexto.TareaProgramadas.FirstOrDefault(x => x.Alias == request.Alias);
                if (tareaProgramada == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                if (tareaProgramada.EnEjecucion)
                {
                    return CommandResult.Fail("Tarea en ejecucion", 400);
                }

                ProcessStartInfo startInfo = new ProcessStartInfo("powershell.exe")
                {
                    WindowStyle = ProcessWindowStyle.Minimized,
                    Arguments = tareaProgramada.Instruccion
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
