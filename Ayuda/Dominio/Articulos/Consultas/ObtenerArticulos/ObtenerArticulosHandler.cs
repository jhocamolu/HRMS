using Ayuda.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ayuda.Dominio.Articulos.Consultas.ObtenerArticulos
{
    public class ObtenerArticulosHandler : IRequestHandler<ObtenerArticulosRequest, IQueryable>
    {
        private readonly AyudaDbContext context;
        public ObtenerArticulosHandler(AyudaDbContext context)
        {
            this.context = context;
        }
        public async Task<IQueryable> Handle(ObtenerArticulosRequest request, CancellationToken cancellationToken)
        {
            return context.Articulos
                .Include(a => a.ArticuloClaves).ThenInclude(ac => ac.Clave);
        }
    }
}
