using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("ClaseAportanteTipoAportante")]
    public class ClaseAportanteTipoAportante : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ClaseAportanteId { get; set; }
        public virtual ClaseAportante ClaseAportante { get; set; }

        [Required]
        public int TipoAportanteId { get; set; }
        public virtual TipoAportante TipoAportante { get; set; }

    }
}
