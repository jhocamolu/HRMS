using ApiV3.Infraestructura.Enumerador;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("TipoLiquidacion")]
    public class TipoLiquidacion : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Codigo { get; set; }

        [Required]
        public int TipoPeriodoId { get; set; }
        public virtual TipoPeriodo TipoPeriodo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Column(TypeName = "text")]
        public string Descripcion { get; set; }

        [Required]
        [DefaultValue("true")]
        public bool FechaManual { get; set; }

        [Required]
        [DefaultValue("False")]
        public bool Contabiliza { get; set; }

        [Required]
        [DefaultValue("False")]
        public bool AplicaPila { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public TipoLiquidacionProceso Proceso { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public OperacionTotalTipoLiqidacion OperacionTotal { get; set; }

        [Required]
        public int ConceptoNominaAgrupadorId { get; set; }
        public virtual ConceptoNomina ConceptoNominaAgrupador { get; set; }

    }
}
