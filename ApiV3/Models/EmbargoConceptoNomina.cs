using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("EmbargoConceptoNomina")]
    public class EmbargoConceptoNomina : AuditoriaRegistro
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int EmbargoId { get; set; }
        public virtual Embargo Embargo { get; set; }

        [Required]
        public int ConceptoNominaId { get; set; }
        public virtual ConceptoNomina ConceptoNomina { get; set; }

    }
}
