using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("ClaseAusentismo")]
    public class ClaseAusentismo : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(5)")]
        public string Codigo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool AfectaDiaPagar { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool AfectaDiaTrabajado { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool RequiereHora { get; set; }

    }
}
