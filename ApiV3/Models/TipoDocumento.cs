using ApiV3.Infraestructura.Enumerador;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("TipoDocumento", Schema = "dbo")]
    public class TipoDocumento : AuditoriaRegistro
    {

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string CodigoPila { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string CodigoDian { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public FormatoValidacion Formato { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Validacion { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string EquivalenteBancario { get; set; }
    }
}
