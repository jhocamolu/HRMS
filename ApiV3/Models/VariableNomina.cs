using ApiV3.Infraestructura.Enumerador;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("VariableNomina")]
    public class VariableNomina : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Codigo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public TipoDatoSql TipoDato { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Tamanio { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Descripcion { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public TipoVariable TipoVariable { get; set; }
    }
}
