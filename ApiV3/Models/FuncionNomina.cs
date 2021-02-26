using ApiV3.Infraestructura.Enumerador;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("FuncionNomina")]
    public class FuncionNomina : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Alias { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Ayuda { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Proceso { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public TipoFuncion TipoFuncion { get; set; }

        [Column(TypeName = "text")]
        public string FuncionParametro { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool ParaCantidad { get; set; }

    }
}
