using Ayuda.Infraestructura.Resultados;
using Ayuda.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ayuda.Dominio.Articulos.Consultas.ObtenerPalabra
{
    public class ObtenerPalabraHandler : IRequestHandler<ObtenerPalabraRequest, QueryResult>
    {

        private readonly AyudaDbContext contexto;
        public ObtenerPalabraHandler(AyudaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<QueryResult> Handle(ObtenerPalabraRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var query = from clave in contexto.Claves
                            join articuloclave in contexto.ArticuloClaves on clave.Id equals articuloclave.ClaveId
                            join articulo in contexto.Articulos on articuloclave.ArticuloId equals articulo.Id
                            join categoria in contexto.Categorias on articulo.CategoriaId equals categoria.Id
                            where clave.Palabra.Contains(request.Palabra)
                                  || articulo.Titulo.Contains(request.Palabra)
                                  || categoria.Nombre.Contains(request.Palabra)
                            select articulo;

                var count = query.Count();

                IQueryable<Articulo> data = query
                           .Include(a => a.ArticuloClaves)
                           .ThenInclude(ac => ac.Clave).OrderBy(x=> x.CategoriaId).ThenBy
                           .Skip((request.Page - 1) * request.Limit)
                           .Take(request.Limit)
                           .Distinct();

                var page = request.Page;

                return QueryResult.Success(page, count, data);
            }
            catch (Exception e)
            {
                return QueryResult.Fail(e.Message);
            }
        }
    }
}
