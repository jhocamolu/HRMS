using ApiV3.Infraestructura.Enumerador;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("CategoriaNovedad")]
    public class CategoriaNovedad : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        public int? ConceptoNominaId { get; set; }
        public virtual ConceptoNomina ConceptoNomina { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public ModuloSistema Modulo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public ClaseCategoriaNovedad Clase { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool UsaParametrizacion { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool RequiereTercero { get; set; }

        [Column(TypeName = "varchar(255)")]
        public UbicacionTerceroCategoriaNovedad? UbicacionTercero { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool ValorEditable { get; set; }

    }
}
