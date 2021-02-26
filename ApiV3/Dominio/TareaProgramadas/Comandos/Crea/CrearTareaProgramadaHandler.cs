using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TareaProgramadas.Comandos.Crea
{
    public class CrearTareaProgramadaHandler : IRequestHandler<CrearTareaProgramadaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public CrearTareaProgramadaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(CrearTareaProgramadaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TareaProgramada tareaProgramada = new TareaProgramada
                {
                    Descripcion = request.Descripcion,
                    EnEjecucion = false,
                    Instruccion = request.Instruccion,
                    Nombre = request.Nombre,
                    Periodicidad = request.Periodicidad,
                };

                this.contexto.TareaProgramadas.Add(tareaProgramada);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(tareaProgramada);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
