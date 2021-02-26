using Ayuda.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ayuda.Dominio.Categorias.Consultas.ObtenerCategorias
{
    public class ObtenerCategoriasHandler : IRequestHandler<ObtenerCategoriasRequest, IQueryable>
    {
        private readonly AyudaDbContext context;
        public ObtenerCategoriasHandler(AyudaDbContext context)
        {
            this.context = context;
        }

        public async Task<IQueryable> Handle(ObtenerCategoriasRequest request, CancellationToken cancellationToken)
        {
            return context.Categorias.Include(a => a.Padre);
        }
    }
}
