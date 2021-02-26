using ApiV3.Infraestructura.Enumerador;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("TipoAusentismo")]
    public class TipoAusentismo : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(5)")]
        public string Codigo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        public int ClaseAusentismoId { get; set; }
        public virtual ClaseAusentismo ClaseAusentismo { get; set; }

        [Required]
        public UnidadTiempo UnidadTiempo { get; set; }

    }
}
