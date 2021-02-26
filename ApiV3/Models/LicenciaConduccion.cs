using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("LicenciaConduccion", Schema = "dbo")]
    public class LicenciaConduccion : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Clase { get; set; }
    }
}
