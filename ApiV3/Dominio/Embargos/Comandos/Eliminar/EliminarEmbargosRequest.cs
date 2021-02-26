using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApiV3.Dominio.Embargos.Comandos.Eliminar
{
    public class EliminarEmbargosRequest : IRequest<CommandResult>
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Id { get; set; }
    }
}