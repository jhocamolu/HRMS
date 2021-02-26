using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Clase encargada de realizar las validaciones para eliminar registros en la entidad Operadores de pago
/// </summary>

namespace ApiV3.Dominio.OperadorPagos.Comando.Eliminar
{
    public class EliminarOperadorPagoRequest : IRequest<CommandResult>
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }

    }
}
