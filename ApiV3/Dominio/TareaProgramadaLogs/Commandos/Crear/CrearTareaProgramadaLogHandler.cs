using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TareaProgramadaLogs.Commandos.Crear
{
    public class CrearTareaProgramadaLogHandler : IRequestHandler<CrearTareaProgramadaLogRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public CrearTareaProgramadaLogHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearTareaProgramadaLogRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var tareaProgramada = this.contexto.TareaProgramadas.FirstOrDefault(x => x.Alias == request.TareaProgramadaAlias);

                TareaProgramadaLog tareaProgramadaLog = new TareaProgramadaLog
                {
                    Resultado = request.Resultado,
                    Estado = (EstadoTareaProgramada)request.Estado,
                    TareaProgramadaId = tareaProgramada.Id,
                };

                this.contexto.TareasProgramadasLogs.Update(tareaProgramadaLog);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(tareaProgramadaLog);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
