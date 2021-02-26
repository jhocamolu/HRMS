using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TareaProgramadas.Comandos.Actualizar
{
    public class ActualizarTareaProgramadaHandler : IRequestHandler<ActualizarTareaProgramadaRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;
        public ActualizarTareaProgramadaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarTareaProgramadaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var tareaProgramada = this.contexto.TareaProgramadas.Find(request.Id);
                tareaProgramada.Instruccion = request.Instruccion;
                tareaProgramada.Nombre = request.Nombre;
                tareaProgramada.Periodicidad = request.Periodicidad;
                tareaProgramada.Descripcion = request.Descripcion;
                tareaProgramada.EnEjecucion = false;


                this.contexto.TareaProgramadas.Update(tareaProgramada);
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
