using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoContratos.Comandos.Eliminar
{
    public class EliminarTipoContratoHandler : IRequestHandler<EliminarTipoContratoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarTipoContratoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarTipoContratoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoContrato tipoContrato = await this.contexto.TipoContratos.FindAsync(request.Id);
                if (tipoContrato == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.TipoContratos.Remove(tipoContrato);
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
