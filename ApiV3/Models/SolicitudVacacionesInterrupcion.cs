using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("SolicitudVacacionesInterrupcion")]
    public class SolicitudVacacionesInterrupcion : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int SolicitudVacacionesId { get; set; }
        public virtual SolicitudVacacion SolicitudVacacion { get; set; }

        [Required]
        public int FuncionarioAusentismoId { get; set; }
        public virtual AusentismoFuncionario AusentismoFuncionario { get; set; }

    }
}
