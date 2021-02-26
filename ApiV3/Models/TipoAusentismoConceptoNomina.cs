using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("TipoAusentismoConceptoNomina")]
    public class TipoAusentismoConceptoNomina : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TipoAusentismoId { get; set; }
        public virtual TipoAusentismo TipoAusentismo { get; set; }

        [Required]
        public int ConceptoNominaId { get; set; }
        public virtual ConceptoNomina ConceptoNomina { get; set; }

        [Required]
        public int CoberturaDesde { get; set; }

        [Required]
        public int CoberturaHasta { get; set; }
    }
}
