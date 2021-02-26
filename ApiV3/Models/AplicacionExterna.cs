using ApiV3.Infraestructura.Enumerador;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("AplicacionExterna")]
    public class AplicacionExterna : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public string Codigo { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Descripcion { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public TipoAplicacionExterna Revisa { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public TipoAplicacionExterna Aprueba { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public TipoAplicacionExterna Autoriza { get; set; }

    }
}
