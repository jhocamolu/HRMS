using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("NotificacionPlantilla", Schema = "dbo")]
    public class NotificacionPlantilla : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Alias { get; set; }

        [Required]
        [Column(TypeName = "Text")]
        public string Descripcion { get; set; }

        [Required]
        [Column(TypeName = "Text")]
        [DataType(DataType.MultilineText)]
        public string Plantilla { get; set; }
    }
}
