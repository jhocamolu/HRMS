using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CategoriaNovedades.Comandos.Eliminar
{
    public class EliminarCategoriaNovedadHandler : IRequestHandler<EliminarCategoriaNovedadRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarCategoriaNovedadHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarCategoriaNovedadRequest request, CancellationToken cancellationToken)
        {
            try
            {
                CategoriaNovedad categoriaNovedad = contexto.CategoriaNovedades.Find(request.Id);
                if (categoriaNovedad == null)
                {
                    CommandResult.Fail("No existe", 404);
                }
                this.contexto.CategoriaNovedades.Remove(categoriaNovedad);
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
