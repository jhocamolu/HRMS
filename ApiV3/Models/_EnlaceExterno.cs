using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("_EnlaceExterno", Schema = "util")]
    public class _EnlaceExterno : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Titulo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Url { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Imagen { get; set; }
    }
}