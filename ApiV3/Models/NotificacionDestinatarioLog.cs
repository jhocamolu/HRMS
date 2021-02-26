using ApiV3.Infraestructura.Enumerador;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("NotificacionDestinatarioLog")]
    public class NotificacionDestinatarioLog : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int NotificacionId { get; set; }
        public virtual Notificacion Notificacion { get; set; }


        public int? FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string CorreoElectronico { get; set; }

        [Column(TypeName = "varchar(255)")]
        public EstadoNotificacion Estado { get; set; }


        [Column(TypeName = "text")]
        public string Resultado { get; set; }
    }
}
