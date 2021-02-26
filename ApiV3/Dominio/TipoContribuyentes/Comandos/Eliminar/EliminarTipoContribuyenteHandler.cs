using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoContribuyentes.Comandos.Eliminar
{
    public class EliminarTipoContribuyenteHandler : IRequestHandler<EliminarTipoContribuyenteRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarTipoContribuyenteHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarTipoContribuyenteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoContribuyente tipoContribuyente = await this.contexto.TipoContribuyentes.FindAsync(request.Id);
                if (tipoContribuyente == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.TipoContribuyentes.Remove(tipoContribuyente);
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
