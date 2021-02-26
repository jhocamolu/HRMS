using ApiV3.Infraestructura.Enumerador;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("TipoGastoViaje")]
    public class TipoGastoViaje : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Description = "Identificador del concepto de nómina del tipo gasto de viaje.")]
        public int ConceptoNominaId { get; set; }
        public virtual ConceptoNomina ConceptoNomina { get; set; }

        [Required]
        [Display(Description = "Tipo de gasto de viaje.")]
        [Column(TypeName = "varchar(255)")]
        public TipoGastosViaje Tipo { get; set; }
    }
}
