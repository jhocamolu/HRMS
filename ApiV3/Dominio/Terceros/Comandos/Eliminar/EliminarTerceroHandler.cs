using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Terceros.Comandos.Eliminar
{
    public class EliminarTerceroHandler : IRequestHandler<EliminarTerceroRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public EliminarTerceroHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarTerceroRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Tercero tercero = await this.contexto.Terceros.FindAsync(request.Id);
                if (tercero == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.Terceros.Remove(tercero);
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
