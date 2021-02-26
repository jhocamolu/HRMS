using ApiV3.Infraestructura.Enumerador;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("ConceptoNominaElementoFormula")]
    public class ConceptoNominaElementoFormula : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ConceptoNominaId { get; set; }
        public virtual ConceptoNomina ConceptoNomina { get; set; }

        [Required]
        public int ElementoFormulaId { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public TipoElementoFormula Tipo { get; set; }
    }
}
