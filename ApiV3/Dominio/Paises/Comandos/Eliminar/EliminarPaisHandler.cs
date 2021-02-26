using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Paises.Comandos.Eliminar
{
    public class EliminarPaisHandler : IRequestHandler<EliminarPaisRequest, CommandResult>
    {
        private readonly NominaDbContext context;

        public EliminarPaisHandler(NominaDbContext context)
        {
            this.context = context;
        }

        public async Task<CommandResult> Handle(EliminarPaisRequest request, CancellationToken cancellationToken)
        {
            Pais pais = await this.context.Paises.FindAsync(request.Id);
            try
            {
                this.context.Paises.Remove(pais);
                await this.context.SaveChangesAsync();
                return CommandResult.Success(pais);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }

        }
    }

}
