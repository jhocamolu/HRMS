using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("NaturalezaJuridica")]
    public class NaturalezaJuridica : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Codigo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }
    }
}
