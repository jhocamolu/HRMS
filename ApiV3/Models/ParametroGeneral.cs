using ApiV3.Infraestructura.Enumerador;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("ParametroGeneral")]
    public class ParametroGeneral : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Alias { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public TipoDato Tipo { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string HtmlOpcion { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Etiqueta { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Ayuda { get; set; }

        [Required]
        [Column(TypeName = "smallint")]
        public int Orden { get; set; }

        [Column(TypeName = "text")]
        public string Item { get; set; }

        [Required]
        public bool Obligatorio { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Valor { get; set; }

        [Required]
        public int AnnoVigenciaId { get; set; }
        public virtual AnnoVigencia AnnoVigencia { get; set; }

        [Required]
        public int CategoriaParametroId { get; set; }
        public virtual CategoriaParametro CategoriaParametro { get; set; }
    }
}
