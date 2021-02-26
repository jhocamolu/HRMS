using ApiV3.Infraestructura.Enumerador;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiV3.Models
{
    [Table("TipoLiquidacionComprobante")]
    public class TipoLiquidacionComprobante : AuditoriaRegistro
    {
        [Key]
        [Required]
        [Display(Description = "llave")]
        public int Id { get; set; }

        [Required]
        public int TipoLiquidacionId { get; set; }
        public virtual TipoLiquidacion TipoLiquidacion { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public TipoComprobante TipoComprobante { get; set; }

        [Required]
        public int CentroCostoId { get; set; }
        public virtual CentroCosto CentroCosto { get; set; }

        [Required]
        public int CuentaContableId { get; set; }
        public virtual CuentaContable CuentaContable { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public NaturalezaContable Naturaleza { get; set; }

    }
}
