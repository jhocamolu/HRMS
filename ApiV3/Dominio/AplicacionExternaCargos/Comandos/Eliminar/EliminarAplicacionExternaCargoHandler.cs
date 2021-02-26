using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.AplicacionExternaCargos.Comandos.Eliminar
{
    public class EliminarAplicacionExternaCargoHandler : IRequestHandler<EliminarAplicacionExternaCargoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarAplicacionExternaCargoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarAplicacionExternaCargoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                AplicacionExternaCargo aplicacionExternaCargo = contexto.AplicacionExternaCargos.Find(request.Id);
                if (aplicacionExternaCargo == null)
                {
                    CommandResult.Fail("No existe", 404);
                }
                this.contexto.AplicacionExternaCargos.Remove(aplicacionExternaCargo);
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
