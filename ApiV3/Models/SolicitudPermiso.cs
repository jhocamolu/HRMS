using ApiV3.Infraestructura.Enumerador;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("SolicitudPermiso")]
    public class SolicitudPermiso : AuditoriaRegistro
    {
        [Key]
        [Display(Description = "llave")]
        public int Id { get; set; }

        [Required]
        [Display(Description = "Documento de identificación del funcionario.")]
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        [Required]
        [Display(Description = "identificación del tipo de ausentismo.")]
        public int TipoAusentismoId { get; set; }
        public virtual TipoAusentismo TipoAusentismo { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime FechaInicio { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime FechaFin { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan? HoraSalida { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan? HoraLlegada { get; set; }

        [Column(TypeName = "text")]
        public string Observaciones { get; set; }

        [Required]
        [Column(TypeName = "char(20)")]
        public EstadoSolicitudPermiso Estado { get; set; }

        [Column(TypeName = "text")]
        public string Justificacion { get; set; }
    }
}
