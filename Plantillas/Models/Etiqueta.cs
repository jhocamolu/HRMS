using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plantillas.Models
{
    [Table("Etiqueta")]
    public class Etiqueta : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(255)]
        public string Slug { get; set; }

        public virtual ICollection<GrupoDocumentoEtiqueta> Etiquetas { get; set; }

    }
}
