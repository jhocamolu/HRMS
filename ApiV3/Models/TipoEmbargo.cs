using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("TipoEmbargo")]
    public class TipoEmbargo : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        public bool SalarioMinimoEmbargable { get; set; }

        [Required]
        public sbyte Prioridad { get; set; }
    }
}
