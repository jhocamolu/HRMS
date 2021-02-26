using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApiV3.Dominio.Idiomas.Consultas.ObtenerIdioma
{
    public class ObtenerIdiomaRequest : IRequest<Idioma>
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; internal set; }
    }
}
