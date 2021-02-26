using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApiV3.Dominio.Paises.Consultas.Obtener
{
    public class ObtenerPaisRequest : IRequest<Pais>
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; internal set; }
    }
}
