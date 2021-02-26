using ApiV3.Infraestructura.Enumerador;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("TipoLiquidacionEstado")]
    public class TipoLiquidacionEstado : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TipoLiquidacionId { get; set; }
        public virtual TipoLiquidacion TipoLiquidacion { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public EstadoFuncionario EstadoFuncionario { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public EstadoContrato EstadoContrato { get; set; }
    }
}
