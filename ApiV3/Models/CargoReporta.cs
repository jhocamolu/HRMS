using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("CargoReporta")]
    public class CargoReporta : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        public int CargoDependenciaId { get; set; }
        public virtual CargoDependencia CargoDependencia { get; set; }

        public int CargoDependenciaReportaId { get; set; }
        public virtual CargoDependencia CargoDependenciaReporta { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool JefeInmediato { get; set; }
    }
}
