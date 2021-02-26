using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Clase encargada de realizar las validaciones para  eliminar registros del modelo JornadaLaborales
/// </summary>

namespace ApiV3.Dominio.JornadaLaborales.Comandos.Eliminar
{
    public class EliminarJornadaLaboralRequest : IRequest<CommandResult>
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
    }
}