using ApiV3.Infraestructura.Enumerador;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("Candidato", Schema = "dbo")]
    public class Candidato : AuditoriaRegistro
    {
        [Key]
        [Required]
        [Display(Description = "Identificador")]
        public int Id { get; set; }

        [Required]
        [Display(Description = "Identificador RequisicionPersonal.")]
        public int RequisicionPersonalId { get; set; }
        public RequisicionPersonal RequisicionPersonal { get; set; }

        [Required]
        [Display(Description = "Identificador HojaDeVida.")]
        public int HojaDeVidaId { get; set; }
        public HojaDeVida HojaDeVida { get; set; }

        [Required]
        [Display(Description = "Estado actual del Candidado.")]
        [Column(TypeName = "varchar(255)")]
        public EstadoCandidato Estado { get; set; }

        [Display(Description = "Breve descripción de la acción realizada.")]
        [Column(TypeName = "text")]
        public string Justificacion { get; set; }

        [Display(Description = "ObjectId correspondiente al PDF con todas las pruebas.")]
        public string AdjuntoPruebas { get; set; }

        [Display(Description = "ObjectId correspondiente al PDF los examenes.")]
        public string AdjuntoExamen { get; set; }
    }
}
