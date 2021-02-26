using ApiV3.Infraestructura.Enumerador;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("TareaProgramadaLog")]
    public class TareaProgramadaLog : AuditoriaRegistro
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Display(Description = "Identificador de la notificación.")]
        public int TareaProgramadaId { get; set; }
        public virtual TareaProgramada TareaProgramada { get; set; }

        [Display(Description = "Indica el estado de  la ejecucion de la tarea.")]
        public EstadoTareaProgramada Estado { get; set; }

        [Column(TypeName = "text")]
        [Display(Description = "Resultado de la ejecucion")]
        public string Resultado { get; set; }
    }
}