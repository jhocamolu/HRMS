using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("ConceptoBase")]
    public class ConceptoBase : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ConceptoNominaAgrupadorId { get; set; }
        public virtual ConceptoNomina ConceptoNominaAgrupador { get; set; }

        [Required]
        public int ConceptoNominaId { get; set; }
        public virtual ConceptoNomina ConceptoNomina { get; set; }

    }
}
