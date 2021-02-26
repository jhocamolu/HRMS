using Plantillas.Dominio.Enumerador;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plantillas.Models
{
    [Table("ComplementoPlantilla")]
    public class ComplementoPlantilla : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }

        [Required]
        public string Alto { get; set; }

        [Required]
        public TipoComplemento Tipo { get; set; }

        [Required]
        public int GrupoDocumentoId { get; set; }
        public virtual GrupoDocumento GrupoDocumento { get; set; }

        [Required]
        [Column(TypeName = "text")]
        [DataType(DataType.MultilineText)]
        public string CuerpoDocumento { get; set; }

        public virtual ICollection<Plantilla> Encabezados { get; set; }

        public virtual ICollection<Plantilla> PiePaginas { get; set; }
    }
}
