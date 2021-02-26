using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiV3.Models
{
    [Table("NominaContabilidad")]
    public class NominaContabilidad : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int NominaFuncionarioId { get; set; }
        public virtual NominaFuncionario NominaFuncionario { get; set; }

        public int? ConceptoNominaId { get; set; }
        public virtual ConceptoNomina ConceptoNomina { get; set; }

        [Required]
        public int CentroCostoId { get; set; }
        public virtual CentroCosto CentroCosto { get; set; }

        [Required]
        public int CuentaContableId { get; set; }
        public virtual CuentaContable CuentaContable { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string TipoComprobante { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nit { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }

        [Column(TypeName = "money")]
        public double? Debito { get; set; }

        [Column(TypeName = "money")]
        public double? Credito { get; set; }
       
    }
}
