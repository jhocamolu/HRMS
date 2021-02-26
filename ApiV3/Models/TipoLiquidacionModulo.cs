using ApiV3.Infraestructura.Enumerador;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("TipoLiquidacionModulo")]
    public class TipoLiquidacionModulo : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TipoLiquidacionId { get; set; }
        public virtual TipoLiquidacion TipoLiquidacion { get; set; }

        [Column(TypeName = "varchar(255)")]
        public ModuloSistema Modulo { get; set; }
    }
}
