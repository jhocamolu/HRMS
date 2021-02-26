using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.HoraExtras.Comandos.Eliminar
{
    public class EliminarHoraExtraHandler : IRequestHandler<EliminarHoraExtraRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarHoraExtraHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarHoraExtraRequest request, CancellationToken cancellationToken)
        {
            try
            {
                HoraExtra horaExtra = this.contexto.HoraExtras.Find(request.Id);
                if (horaExtra == null)
                {
                    CommandResult.Fail("No existe", 404);
                }
                this.contexto.HoraExtras.Remove(horaExtra);
                await contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
