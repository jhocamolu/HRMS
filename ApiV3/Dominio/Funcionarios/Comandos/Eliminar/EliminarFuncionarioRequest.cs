using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Clase encargada de realizar validaciones para eliminar registros de la entidad Funcionarios.
/// </summary>

namespace ApiV3.Dominio.Funcionarios.Comandos.Eliminar
{
    public class EliminarFuncionarioRequest : IRequest<CommandResult>
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
    }
}
