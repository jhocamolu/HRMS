using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plantillas.Models
{
    [Table("Documento")]
    public class Documento : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }

        public string Slug { get; set; }

        [Required]
        public int GrupoDocumentoId { get; set; }
        public virtual GrupoDocumento GrupoDocumentos { get; set; }

    }
}