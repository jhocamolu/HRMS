using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.EstadosCiviles.Consultas.ObtenerEstadoCivil
{
    public class ObtenerEstadoCivilRequest : IRequest<IQueryable>
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        public int Id { get; internal set; }
    }
}
