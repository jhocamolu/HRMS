
using MediatR;
using Reclutamiento.Infraestructura.Resultados;
using Reclutamiento.Infraestructura.Utilidades;
using System.ComponentModel.DataAnnotations;

namespace Reclutamiento.Dominio.HojaDeVidaEstudios.Comandos.Eliminar
{
    public class EliminarHojaDeVidaEstudioRequest : IRequest<CommandResult>
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
    }
}

