using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("SubPeriodo")]
    public class SubPeriodo : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TipoPeriodoId { get; set; }
        public virtual TipoPeriodo TipoPeriodo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        public int Dias { get; set; }

        [Required]
        public int DiaInicial { get; set; }
    }
}
