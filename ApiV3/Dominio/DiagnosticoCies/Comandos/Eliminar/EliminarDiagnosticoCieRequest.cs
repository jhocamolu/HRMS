using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Clase encargada de realizar las validaciones de formato para la eliminar registros
/// de la entidad DiagnosticoCie
/// </summary>

namespace ApiV3.Dominio.DiagnosticoCies.Comandos.Eliminar
{
    public class EliminarDiagnosticoCieRequest : IRequest<CommandResult>
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
    }
}
