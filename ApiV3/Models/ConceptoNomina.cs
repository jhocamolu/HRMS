using ApiV3.Infraestructura.Enumerador;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("ConceptoNomina")]
    public class ConceptoNomina : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Codigo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Alias { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public TipoConceptoNomina TipoConceptoNomina { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public ClaseConceptoNomina ClaseConceptoNomina { get; set; }

        [Required]
        public int Orden { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool ConceptoAgrupador { get; set; }


        [Required]
        [Column(TypeName = "varchar(255)")]
        public OrigenCentroCostoNomina OrigenCentroCosto { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public OrigenTerceroNomina OrigenTercero { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool VisibleImpresion { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public UnidadMedida UnidadMedida { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool RequiereCantidad { get; set; }

        public int? FuncionNominaId { get; set; }
        public virtual FuncionNomina FuncionNomina { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string NitTercero { get; set; }

        [Column(TypeName = "varchar(255)")]
        public int? DigitoVerificacion { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Descripcion { get; set; }

        [Column(TypeName = "text")]
        public string Formula { get; set; }

        [Column(TypeName = "text")]
        public string ProcedimientoSql { get; set; }

        [Column(TypeName = "varchar(MAX)")]
        public string ProcedimientoNombre { get; set; }

    }
}
