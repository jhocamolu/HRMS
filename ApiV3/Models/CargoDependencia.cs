using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("CargoDependencia")]
    public class CargoDependencia : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        public int CargoId { get; set; }
        public virtual Cargo Cargo { get; set; }

        public int DependenciaId { get; set; }
        public virtual Dependencia Dependencia { get; set; }

        public virtual ICollection<CargoReporta> MeReportan { get; set; }

    }
}
