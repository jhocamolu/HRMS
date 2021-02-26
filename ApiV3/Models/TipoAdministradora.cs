using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("TipoAdministradora")]
    public class TipoAdministradora : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Codigo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "decimal(19,3)")]
        public double TarifaAporteFuncionario { get; set; }

        [Required]
        [Column(TypeName = "decimal(19,3)")]
        public double TarifaAporteEmpresa { get; set; }

        [Column(TypeName = "decimal(19,3)")]
        public double? TarifaAporteTotal { get; set; }
    }
}
