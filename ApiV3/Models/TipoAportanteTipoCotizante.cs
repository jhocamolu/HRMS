using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("TipoAportanteTipoCotizante")]
    public class TipoAportanteTipoCotizante : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TipoAportanteId { get; set; }
        public virtual TipoAportante TipoAportante { get; set; }

        [Required]
        public int TipoCotizanteId { get; set; }
        public virtual TipoCotizante TipoCotizante { get; set; }

    }
}
