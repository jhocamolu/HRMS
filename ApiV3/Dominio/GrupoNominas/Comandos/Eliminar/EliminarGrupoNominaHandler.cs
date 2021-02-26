using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.GrupoNominas.Comandos.Eliminar
{
    public class EliminarGrupoNominaHandler : IRequestHandler<EliminarGrupoNominaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarGrupoNominaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarGrupoNominaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                GrupoNomina grupoNomina = contexto.GrupoNominas.Find(request.Id);
                if (grupoNomina == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                this.contexto.GrupoNominas.Remove(grupoNomina);
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
