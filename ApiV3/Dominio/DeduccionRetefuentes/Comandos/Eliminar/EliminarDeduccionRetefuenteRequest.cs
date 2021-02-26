using ApiV3.Infraestructura.Resultados;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApiV3.Dominio.DeduccionRetefuentes.Comandos.Eliminar
{
    public class EliminarDeduccionRetefuenteRequest : IRequest<CommandResult>
    {
        [Required]
        public int Id { get; set; }
    }
}
