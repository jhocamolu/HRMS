using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Dependencias.Comandos.Eliminar
{
    public class EliminarDependenciaHandler : IRequestHandler<EliminarDependenciaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarDependenciaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarDependenciaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Dependencia dependencia = this.contexto.Dependencias.Find(request.Id);

                contexto.Dependencias.Remove(dependencia);

                await contexto.SaveChangesAsync();

                DependenciaJerarquia dependenciaJerarquia = this.contexto.DependenciaJerarquias
                                                                .FirstOrDefault(x => x.DependenciaHijoId == dependencia.Id);
                contexto.DependenciaJerarquias.Remove(dependenciaJerarquia);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(dependencia);

            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }

        }
    }
}
