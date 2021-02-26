using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Administradoras.Comandos.Eliminar
{
    public class EliminarAdministradoraHandler : IRequestHandler<EliminarAdministradoraRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarAdministradoraHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarAdministradoraRequest request, CancellationToken cancellationToken)
        {
            Administradora administradora = await this.contexto.Administradoras.FindAsync(request.Id);
            try
            {
                this.contexto.Administradoras.Remove(administradora);
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
