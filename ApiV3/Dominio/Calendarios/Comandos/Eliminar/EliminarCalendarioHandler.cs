using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Calendarios.Comandos.Eliminar
{
    public class EliminarCalendarioHandler : IRequestHandler<EliminarCalendarioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarCalendarioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarCalendarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Calendario calendario = this.contexto.Calendarios.FirstOrDefault(x => x.Id == request.Id);
                contexto.Calendarios.Remove(calendario);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(calendario);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}