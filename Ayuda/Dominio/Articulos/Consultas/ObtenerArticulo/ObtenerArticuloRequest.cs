using Ayuda.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ayuda.Dominio.Articulos.Consultas.ObtenerArticulo
{
    public class ObtenerArticuloRequest: IRequest<Articulo>
    {
        //[Required(ErrorMessage = "Este campo es requerido")]
        public int Id { get; set; }
    }
}
