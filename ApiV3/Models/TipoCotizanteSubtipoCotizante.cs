using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("TipoCotizanteSubtipoCotizante")]
    public class TipoCotizanteSubtipoCotizante : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TipoCotizanteId { get; set; }
        public virtual TipoCotizante TipoCotizante { get; set; }

        [Required]
        public int SubtipoCotizanteId { get; set; }
        public virtual SubtipoCotizante SubtipoCotizante { get; set; }

        public bool? AportaASalud { get; set; }
        public bool? AportaAPension { get; set; }
        public bool? AportaAArl { get; set; }
        public bool? AportaACcf { get; set; }
        public bool? AportaASena { get; set; }
        public bool? AportaAIcbf { get; set; }


    }
}
