using Ayuda.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ayuda.Dominio.Categorias.Consultas.ObtenerCategoria
{
    public class ObtenerCategoriaRequest : IRequest<Categoria>
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        public int Id { get; internal set; }
    }
}
