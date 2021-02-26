using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ayuda.Dominio.Categorias.Consultas.ObtenerCategorias
{
    public class ObtenerCategoriasRequest: IRequest<IQueryable>
    {
    }
}
