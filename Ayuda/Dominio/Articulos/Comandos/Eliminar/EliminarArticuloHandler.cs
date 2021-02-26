using Ayuda.Infraestructura.Resultados;
using Ayuda.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ayuda.Dominio.Articulos.Comandos.Eliminar
{
    public class EliminarArticuloHandler : IRequestHandler<EliminarArticuloRequest, CommandResult>
    {
        private readonly AyudaDbContext context;
        public EliminarArticuloHandler(AyudaDbContext context)
        {
            this.context = context;
        }

        public async Task<CommandResult> Handle(EliminarArticuloRequest request, CancellationToken cancellationToken)
        {
            Articulo articulo = await context.Articulos.FindAsync(request.Id);
            try
            {
                int eliminarClaves = await context.Database.ExecuteSqlCommandAsync($"DELETE FROM ArticuloClave WHERE ArticuloId={request.Id}");
                if (eliminarClaves != 0)
                {
                    context.Articulos.Remove(articulo);
                    await context.SaveChangesAsync();
                }

            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
            return CommandResult.Success(articulo);
        }
    }
}
