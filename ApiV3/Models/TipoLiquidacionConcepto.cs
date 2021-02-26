using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("TipoLiquidacionConcepto")]
    public class TipoLiquidacionConcepto : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TipoliquidacionId { get; set; }
        public virtual TipoLiquidacion TipoLiquidacion { get; set; }

        [Required]
        public int ConceptoNominaId { get; set; }
        public virtual ConceptoNomina ConceptoNomina { get; set; }

        public int? TipoContratoId { get; set; }
        public virtual TipoContrato TipoContrato { get; set; }

        [Required]
        public int SubPeriodoId { get; set; }
        public virtual SubPeriodo SubPeriodo { get; set; }
    }
}
