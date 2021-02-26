using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Plantillas.Models
{
    [Table("GrupoDocumento")]
    public class GrupoDocumento : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(255)]
        public string Slug { get; set; }

        [Required]
        [StringLength(1000)]
        public string Servicio { get; set; }

        public virtual ICollection<GrupoDocumentoEtiqueta> GrupoEtiquetaDocumento { get; set; }

        [IgnoreDataMember]
        public virtual ICollection<Plantilla> Plantilla { get; set; }
    }
}