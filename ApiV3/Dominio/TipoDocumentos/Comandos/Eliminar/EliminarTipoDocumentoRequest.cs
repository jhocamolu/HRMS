using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Clase encargada de la validacion de los campos para Eliminar. 
/// </summary>

namespace ApiV3.Dominio.TipoDocumentos.Comandos.Eliminar
{
    public class EliminarTipoDocumentoRequest : IRequest<CommandResult>
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
    }
}
