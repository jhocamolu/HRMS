using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("ClaseAportanteTipoCotizante")]
    public class ClaseAportanteTipoCotizante : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClaseAportanteId { get; set; }
        public virtual ClaseAportante ClaseAportante { get; set; }

        [Required]
        public int TipoCotizanteId { get; set; }
        public virtual TipoCotizante TipoCotizante { get; set; }

    }
}
