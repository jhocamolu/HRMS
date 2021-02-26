using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("SoporteSolicitudPermiso")]
    public class SoporteSolicitudPermiso : AuditoriaRegistro
    {
        [Key]
        [Display(Description = "llave")]
        public int Id { get; set; }

        [Required]
        [Display(Description = "identificación de la solicitud del permiso.")]
        public int SolicitudPermisoId { get; set; }
        public virtual SolicitudPermiso SolicitudPermiso { get; set; }

        [Required]
        [Display(Description = "identificación de la solicitud del permiso.")]
        public int TipoSoporteId { get; set; }
        public virtual TipoSoporte TipoSoporte { get; set; }

        [Column(TypeName = "text")]
        public string Comentario { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Adjunto { get; set; }
    }
}
