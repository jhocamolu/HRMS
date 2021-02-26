using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de eliminar registros de la entidad NivelEducativo
/// </summary>

namespace ApiV3.Dominio.NivelEducativos.Comandos.Eliminar
{
    public class EliminarNivelEducativoHandler : IRequestHandler<EliminarNivelEducativoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public EliminarNivelEducativoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarNivelEducativoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                NivelEducativo nivelEducativo = await this.contexto.NivelEducativos.FindAsync(request.Id);
                if (nivelEducativo == null) return CommandResult.Fail("No existe", 404);

                this.contexto.NivelEducativos.Remove(nivelEducativo);
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
