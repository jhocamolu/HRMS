using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plantillas.Models
{
    [Table("GrupoDocumentoEtiqueta")]
    public class GrupoDocumentoEtiqueta : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int GrupoDocumentoId { get; set; }
        public virtual GrupoDocumento GrupoDocumento { get; set; }

        [Required]
        public int EtiquetaId { get; set; }
        public virtual Etiqueta Etiqueta { get; set; }

        public int? ServicioFijoId { get; set; }
        public virtual ServicioFijo ServicioFijo { get; set; }

        [Required]
        [StringLength(255)]
        public string Campo { get; set; }

    }
}
