using ApiV3.Infraestructura.Resultados;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApiV3.Dominio.Contratos.Comandos.Eliminar
{
    public class EliminarContratoRequest : IRequest<CommandResult>
    {
        [Required]
        public int Id { get; set; }
    }
}
