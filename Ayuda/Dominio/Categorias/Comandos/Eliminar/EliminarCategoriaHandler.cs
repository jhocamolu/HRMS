using Ayuda.Infraestructura.Resultados;
using Ayuda.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ayuda.Dominio.Categorias.Comandos.Eliminar
{
    public class EliminarCategoriaHandler : IRequestHandler<EliminarCategoriaRequest, CommandResult>
    {
        private readonly AyudaDbContext context;
        public EliminarCategoriaHandler(AyudaDbContext context)
        {
            this.context = context;
        }
        public async Task<CommandResult> Handle(EliminarCategoriaRequest request, CancellationToken cancellationToken)
        {

            Categoria categoria = await context.Categorias.FindAsync(request.Id);
            try
            {
                context.Categorias.Remove(categoria);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {

            }
            return CommandResult.Success(categoria);
        }
    }
}
