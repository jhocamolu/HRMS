using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Parentescos.Comandos.Eliminar
{
    public class EliminarParentescoHandler : IRequestHandler<EliminarParentescoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public EliminarParentescoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarParentescoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Parentesco parentesco = await this.contexto.Parentescos.FindAsync(request.Id);
                if (parentesco == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.Parentescos.Remove(parentesco);
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
