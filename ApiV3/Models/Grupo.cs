using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("Grupo")]
    public class Grupo : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        public int Orden { get; set; }

    }
}
