using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("TipoEmbargoConceptoNomina")]
    public class TipoEmbargoConceptoNomina : AuditoriaRegistro
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int TipoEmbargoId { get; set; }
        public virtual TipoEmbargo TipoEmbargo { get; set; }

        [Required]
        public int ConceptoNominaId { get; set; }
        public virtual ConceptoNomina ConceptoNomina { get; set; }

        [Required]
        public double MaximoEmbargarConcepto { get; set; }
    }
}
