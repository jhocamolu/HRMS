using ApiV3.Infraestructura.Enumerador;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("PeriodoContable")]
    public class PeriodoContable : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }

        [Required]
        [Column(TypeName = "char(10)")]
        public EstadoPeriodoContable Estado { get; set; }
    }
}
