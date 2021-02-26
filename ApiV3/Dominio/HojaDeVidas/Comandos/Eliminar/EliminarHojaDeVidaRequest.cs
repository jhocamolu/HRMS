using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApiV3.Dominio.HojaDeVidas.Comandos.Eliminar
{
    public class EliminarHojaDeVidaRequest : IRequest<CommandResult>
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
    }
}
