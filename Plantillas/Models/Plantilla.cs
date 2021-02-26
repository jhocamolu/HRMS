using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plantillas.Models
{
    [Table("Plantilla")]
    public class Plantilla : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }

        [StringLength(255)]
        public string Version { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime FechaVigencia { get; set; }

        [Required]
        public int GrupoDocumentoId { get; set; }
        public virtual GrupoDocumento GrupoDocumento { get; set; }


        public int? EncabezadoId { get; set; }
        public virtual ComplementoPlantilla ComplementoEncabezado { get; set; }

        public int? PiePaginaId { get; set; }
        public virtual ComplementoPlantilla ComplementoPiePagina { get; set; }

        [Required]
        public int DocumentoId { get; set; }
        public virtual Documento Documento { get; set; }

        [Required]
        [Column(TypeName = "text")]
        [DataType(DataType.MultilineText)]
        public string CuerpoDocumento { get; set; }

    }
}
