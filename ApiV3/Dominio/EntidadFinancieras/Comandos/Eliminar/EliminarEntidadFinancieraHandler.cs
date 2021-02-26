using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.EntidadesFinancieras.Comandos.Eliminar
{
    public class EliminarEntidadFinancieraHandler : IRequestHandler<EliminarEntidadFinancieraRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarEntidadFinancieraHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarEntidadFinancieraRequest request, CancellationToken cancellationToken)
        {
            try
            {
                EntidadFinanciera entidadFinanciera = await this.contexto.EntidadFinancieras.FindAsync(request.Id);
                if (entidadFinanciera == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.EntidadFinancieras.Remove(entidadFinanciera);
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
