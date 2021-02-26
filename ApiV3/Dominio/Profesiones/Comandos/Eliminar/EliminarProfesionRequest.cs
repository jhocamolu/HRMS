using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System.ComponentModel.DataAnnotations;

/// <summary>
///  Clase encargada de realizar las validaciones para eliminar registroas de la entidad Profesion.
/// </summary>

namespace ApiV3.Dominio.Profesiones.Comandos.Eliminar
{
    public class EliminarProfesionRequest : IRequest<CommandResult>
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
    }
}