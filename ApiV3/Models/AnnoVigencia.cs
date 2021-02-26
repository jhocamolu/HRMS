using ApiV3.Infraestructura.Enumerador;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("AnnoVigencia")]
    public class AnnoVigencia : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Anno { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public EstadoAnnoVigencia Estado { get; set; }
    }
}
