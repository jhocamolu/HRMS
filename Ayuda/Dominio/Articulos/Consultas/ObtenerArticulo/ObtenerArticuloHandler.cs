using Ayuda.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ayuda.Dominio.Articulos.Consultas.ObtenerArticulo
{
    public class ObtenerArticuloHandler : IRequestHandler<ObtenerArticuloRequest, Articulo>
    {
        private readonly AyudaDbContext context;
        public ObtenerArticuloHandler(AyudaDbContext context)
        {
            this.context = context;
        }

        public async Task<Articulo> Handle(ObtenerArticuloRequest request, CancellationToken cancellationToken)
        {
            return await context.Articulos
                .Include(c => c.Categoria)
                .Include(a => a.ArticuloClaves)
                .ThenInclude(ac => ac.Clave)
                .FirstOrDefaultAsync(a => a.Id == request.Id);
        }
    }
}
