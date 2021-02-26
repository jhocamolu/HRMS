using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("TipoPlanilla")]
    public class TipoPlanilla : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "char(1)")]
        public string Codigo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Column(TypeName = "bit")]
        public bool RequiereNumeroPlanilla { get; set; }

        [Column(TypeName = "bit")]
        public bool RequiereFechaPagoPlanilla { get; set; }

    }
}
