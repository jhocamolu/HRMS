using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Sustitutos.Comandos.Eliminar
{
    public class EliminarSustitutoHandler : IRequestHandler<EliminarSustitutoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarSustitutoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(EliminarSustitutoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Sustituto sustituto = await this.contexto.Sustitutos.FindAsync(request.Id);
                if (sustituto == null) return CommandResult.Fail("No existe", 404);

                this.contexto.Sustitutos.Remove(sustituto);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }

        }

    }
}
