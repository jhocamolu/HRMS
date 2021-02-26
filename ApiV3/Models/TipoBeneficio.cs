using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("TipoBeneficio")]
    public class TipoBeneficio : AuditoriaRegistro
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        public int? ConceptoNominaDevengoId { get; set; }
        public virtual ConceptoNomina ConceptoNominaDevengo { get; set; }

        public int? ConceptoNominaDeduccionId { get; set; }
        public virtual ConceptoNomina ConceptoNominaDeduccion { get; set; }

        public int? ConceptoNominaCalculoId { get; set; }
        public virtual ConceptoNomina ConceptoNominaCalculo { get; set; }

        [Required]
        public bool RequiereAprobacionJefe { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public double MontoMaximo { get; set; }

        [Required]
        public bool ValorSolicitado { get; set; }

        [Required]
        public bool PlazoMes { get; set; }

        [Required]
        public int CuotaPermitida { get; set; }

        [Required]
        public bool PeriodoPago { get; set; }

        public bool PermisoEstudio { get; set; }

        [Required]
        public bool PermiteAuxilioEducativo { get; set; }

        [Required]
        public bool PermiteDescuentoNomina { get; set; }

        [Required]
        public bool ValorAutorizado { get; set; }

        [Required]
        public int DiasAntiguedad { get; set; }

        [Required]
        public int VecesAnio { get; set; }

        [Column(TypeName = "text")]
        public string Descripcion { get; set; }

    }
}
