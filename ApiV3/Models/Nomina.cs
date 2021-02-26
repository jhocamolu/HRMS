using ApiV3.Infraestructura.Enumerador;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("Nomina")]
    public class Nomina : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        public int PeriodoContableId { get; set; }
        public virtual PeriodoContable PeriodoContable { get; set; }

        [Required]
        public int TipoLiquidacionId { get; set; }
        public virtual TipoLiquidacion TipoLiquidacion { get; set; }

        [Required]
        public int SubperiodoId { get; set; }
        public virtual SubPeriodo SubPeriodo { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime FechaInicio { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime FechaFinal { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaAplicacion { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public EstadoNomina Estado { get; set; }
    }
}
