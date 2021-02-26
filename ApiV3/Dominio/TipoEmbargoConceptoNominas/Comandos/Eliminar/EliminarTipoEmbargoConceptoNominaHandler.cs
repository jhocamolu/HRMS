using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoEmbargoConceptoNominas.Comandos.Eliminar
{
    public class EliminarTipoEmbargoConceptoNominaHandler : IRequestHandler<EliminarTipoEmbargoConceptoNominaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarTipoEmbargoConceptoNominaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarTipoEmbargoConceptoNominaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoEmbargoConceptoNomina tipoEmbargoConceptoNomina = await this.contexto.TipoEmbargoConceptoNominas.FindAsync(request.Id);
                if (tipoEmbargoConceptoNomina == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.TipoEmbargoConceptoNominas.Remove(tipoEmbargoConceptoNomina);
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
