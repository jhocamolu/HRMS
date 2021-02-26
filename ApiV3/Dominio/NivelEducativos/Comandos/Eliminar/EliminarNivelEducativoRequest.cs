using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Clase encargada de realizar las validaciones para eliminar registros
/// de la entidad NivelEducativo
/// </summary>

namespace ApiV3.Dominio.NivelEducativos.Comandos.Eliminar
{
    public class EliminarNivelEducativoRequest : IRequest<CommandResult>
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
    }
}
