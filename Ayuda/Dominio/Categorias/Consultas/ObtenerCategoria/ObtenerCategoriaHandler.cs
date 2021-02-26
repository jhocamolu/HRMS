using Ayuda.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ayuda.Dominio.Categorias.Consultas.ObtenerCategoria
{
    public class ObtenerCategoriaHandler : IRequestHandler<ObtenerCategoriaRequest, Categoria>
    {
        private readonly AyudaDbContext context;
        public ObtenerCategoriaHandler(AyudaDbContext context)
        {
            this.context = context;
        }
        public async Task<Categoria> Handle(ObtenerCategoriaRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
