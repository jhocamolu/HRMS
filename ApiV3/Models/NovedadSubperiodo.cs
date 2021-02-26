using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("NovedadSubperiodo")]
    public class NovedadSubperiodo : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int NovedadId { get; set; }
        public virtual Novedad Novedad { get; set; }

        [Required]
        public int SubperiodoId { get; set; }
        public virtual SubPeriodo Subperiodo { get; set; }
    }
}
