using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("MotivoSolicitudCesantia")]
    public class MotivoSolicitudCesantia : AuditoriaRegistro
    {
        [Key]
        [Required]
        [Display(Description = "llave")]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }
    }
}
