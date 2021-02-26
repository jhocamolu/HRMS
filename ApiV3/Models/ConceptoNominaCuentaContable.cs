using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("ConceptoNominaCuentaContable")]
    public class ConceptoNominaCuentaContable : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ConceptoNominaId { get; set; }
        public virtual ConceptoNomina ConceptoNomina { get; set; }

        public int? CentroCostoId { get; set; }
        public virtual CentroCosto CentroCosto { get; set; }

        [Required]
        public int CuentaContableId { get; set; }
        public virtual CuentaContable CuentaContable { get; set; }

    }
}
