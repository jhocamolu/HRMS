using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("BeneficioSubperiodo")]
    public class BeneficioSubperiodo : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public int BeneficioId { get; set; }
        public virtual Beneficio Beneficio { get; set; }


        [Required]
        public int SubPeriodoId { get; set; }
        public virtual SubPeriodo SubPeriodo { get; set; }
    }
}
