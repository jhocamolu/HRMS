using ApiV3.Infraestructura.Enumerador;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("SolicitudVacaciones")]
    public class SolicitudVacacion : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        [Required]
        public int LibroVacacionesId { get; set; }
        public virtual LibroVacacion LibroVacaciones { get; set; }

        public int? NominaFuncionarioId { get; set; }
        public virtual NominaFuncionario NominaFuncionario { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime FechaInicioDisfrute { get; set; }

        [Required]
        [Column(TypeName = "tinyint")]
        public int DiasDisfrute { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime FechaFinDisfrute { get; set; }

        [Column(TypeName = "tinyint")]
        public int DiasDinero { get; set; }

        [Column(TypeName = "text")]
        public string Observacion { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public EstadoSolicitudVacaciones Estado { get; set; }

        [Column(TypeName = "text")]
        public string Justificacion { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime? FechaPago { get; set; }

        [Column(TypeName = "money")]
        public double? Remuneracion { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime? FechaRegreso { get; set; }

    }
}
