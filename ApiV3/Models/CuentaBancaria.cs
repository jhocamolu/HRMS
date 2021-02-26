using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("CuentaBancaria")]
    public class CuentaBancaria : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EntidadFinancieraId { get; set; }
        public virtual EntidadFinanciera EntidadFinanciera { get; set; }

        [Required]
        public int TipoCuentaId { get; set; }
        public virtual TipoCuenta TipoCuenta { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Numero { get; set; }
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }
    }
}
