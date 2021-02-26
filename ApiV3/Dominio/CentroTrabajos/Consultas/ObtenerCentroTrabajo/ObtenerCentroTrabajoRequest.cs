using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApiV3.Dominio.CentroTrabajos.Consultas.ObtenerCentroTrabajo
{
    public class ObtenerCentroTrabajoRequest : IRequest<CentroTrabajo>
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; internal set; }
    }
}
