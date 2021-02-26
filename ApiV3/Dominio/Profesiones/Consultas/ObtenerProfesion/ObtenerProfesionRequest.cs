using ApiV3.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApiV3.Dominio.Profesiones.Consultas.ObtenerProfesion
{
    public class ObtenerProfesionRequest : IRequest<Profesion>
    {
        [Required(ErrorMessage = "Campo requerido")]
        public int Id { get; internal set; }

        [Required(ErrorMessage = "Campo requerido")]
        [MaxLength(60, ErrorMessage = "Maximo 60 caracteres")]
        [RegularExpression(@"[ A-Za-zäÄëËïÏöÖüÜáéíóúáéíóúÁÉÍÓÚÂÊÎÔÛâêîôûàèìòùÀÈÌÒÙñÑ.-]+",
                            ErrorMessage = "El Campo puede ser Alfabético castellano con mayúsculas, acento y espacios")]
        public string Nombre { get; set; }
    }
}
