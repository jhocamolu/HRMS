using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("CausalTerminacion")]
    public class CausalTerminacion : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "char(3)")]
        public string Codigo { get; set; }

        [Required]
        public bool JustaCausa { get; set; }
    }
}
