using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("CentroTrabajo")]
    public class CentroTrabajo : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Codigo { get; set; }

        [StringLength(255)]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        public double PorcentajeRiesgo { get; set; }
    }
}
