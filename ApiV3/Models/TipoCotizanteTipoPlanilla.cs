using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("TipoCotizanteTipoPlanilla")]
    public class TipoCotizanteTipoPlanilla : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TipoCotizanteId { get; set; }
        public virtual TipoCotizante TipoCotizante { get; set; }

        [Required]
        public int TipoPlanillaId { get; set; }
        public virtual TipoPlanilla TipoPlanilla { get; set; }

    }
}
