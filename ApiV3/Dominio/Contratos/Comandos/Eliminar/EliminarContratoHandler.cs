using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Contratos.Comandos.Eliminar
{
    public class EliminarContratoHandler : IRequestHandler<EliminarContratoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarContratoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(EliminarContratoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Contrato contrato = await this.contexto.Contratos.FindAsync(request.Id);
                if (contrato == null)
                {
                    CommandResult.Fail("No existe", 404);
                }
                this.contexto.Contratos.Remove(contrato);
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
