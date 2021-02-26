using ApiV3.Infraestructura.Enumerador;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("TipoContrato")]
    public class TipoContrato : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        public int CantidadProrrogas { get; set; }

        [Required]
        public int DuracionMaxima { get; set; }

        [Required]
        public bool TerminoIndefinido { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public ClaseTipoContrato Clase { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string DocumentoSlug { get; set; }
    }
}
