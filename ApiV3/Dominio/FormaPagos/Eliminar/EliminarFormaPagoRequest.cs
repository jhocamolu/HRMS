using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System.ComponentModel.DataAnnotations;

/// <summary>
///  Clase encargada de realizar las validaciones para eliminar formas de pago.
/// </summary>

namespace ApiV3.Dominio.FormaPagos.Eliminar
{
    public class EliminarFormaPagoRequest : IRequest<CommandResult>
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
    }
}
