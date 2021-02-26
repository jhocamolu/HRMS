using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("TipoBeneficioRequisito")]
    public class TipoBeneficioRequisito : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TipoBeneficioId { get; set; }
        public virtual TipoBeneficio TipoBeneficio { get; set; }

        [Required]
        public int TipoSoporteId { get; set; }
        public virtual TipoSoporte TipoSoporte { get; set; }
    }
}
