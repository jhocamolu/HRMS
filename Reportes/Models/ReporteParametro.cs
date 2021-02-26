using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Reportes.Models
{
    [Table("ReporteParametro")]
    public class ReporteParametro : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ReporteId { get; set; }
        public virtual Reporte Reporte { get; set; }

        [Required]
        public int ParametroId { get; set; }
        public virtual Parametro Parametro { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool EsRequerido { get; set; }

    }
}
