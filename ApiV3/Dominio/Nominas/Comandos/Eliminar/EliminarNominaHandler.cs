using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Nominas.Comandos.Eliminar
{
    public class EliminarNominaHandler : IRequestHandler<EliminarNominaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarNominaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarNominaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Nomina nomina = await this.contexto.Nominas.FindAsync(request.Id);
                if (nomina == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.Nominas.Remove(nomina);
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
