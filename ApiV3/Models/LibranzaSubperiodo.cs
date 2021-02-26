using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("LibranzaSubperiodo")]
    public class LibranzaSubperiodo : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        public int LibranzaId { get; set; }
        public virtual Libranza Libranza { get; set; }

        public int SubPeriodoId { get; set; }
        public virtual SubPeriodo SubPeriodo { get; set; }
    }
}
