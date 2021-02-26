using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("DiagnosticoCie")]
    public class DiagnosticoCie : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string Codigo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

    }
}
