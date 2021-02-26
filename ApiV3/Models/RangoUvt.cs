using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("RangoUvt")]
    public class RangoUvt : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Desde { get; set; }

        public int? Hasta { get; set; }

        [Required]
        [Column(TypeName = "decimal(19, 6)")]
        public decimal Porcentaje { get; set; }

        [Required]
        public int Adiciona { get; set; }

        [Required]
        public int Sustrae { get; set; }

        [Required]
        [Column(TypeName = "smalldatetime")]
        public DateTime ValidoDesde { get; set; }
    }
}
