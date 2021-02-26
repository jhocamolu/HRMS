using ApiV3.Infraestructura.Enumerador;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiV3.Models
{
    [Table("CargoCentroCosto")]
    public class CargoCentroCosto : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CargoId { get; set; }
        public virtual Cargo Cargo { get; set; }

        [Column(TypeName = "decimal(16,6)")]
        public double? Porcentaje { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime FechaCorte { get; set; }

        [Required]
        public int ActividadCentroCostoId { get; set; }
        public virtual ActividadCentroCosto ActividadCentroCosto { get; set; }

        [Required]
        public int CentroOperativoId { get; set; }
        public virtual CentroOperativo CentroOperativo { get; set; }

    }
}
