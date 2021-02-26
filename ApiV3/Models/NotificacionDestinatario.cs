using ApiV3.Infraestructura.Enumerador;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("NotificacionDestinatario", Schema = "dbo")]
    public class NotificacionDestinatario : AuditoriaRegistro
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
    }
}
