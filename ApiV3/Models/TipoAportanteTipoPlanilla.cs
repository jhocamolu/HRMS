using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("TipoAportanteTipoPlanilla")]
    public class TipoAportanteTipoPlanilla : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TipoAportanteId { get; set; }
        public virtual TipoAportante TipoAportante { get; set; }

        [Required]
        public int TipoPlanillaId { get; set; }
        public virtual TipoPlanilla TipoPlanilla { get; set; }

    }
}
