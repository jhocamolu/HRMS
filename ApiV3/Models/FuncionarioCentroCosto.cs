using ApiV3.Infraestructura.Enumerador;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace ApiV3.Models
{
    [Table("FuncionarioCentroCosto")]
    public class FuncionarioCentroCosto : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        [Required]
        public int ActividadCentroCostoId { get; set; }
        public virtual ActividadCentroCosto ActividadCentroCosto { get; set; }

        public int? Cantidad { get; set; }

        [Column(TypeName = "decimal(16,6)")]
        public double? Ponderado { get; set; }

        [Column(TypeName = "decimal(16,6)")]
        public double? Porcentaje { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime FechaCorte { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public EstadoFuncionarioCentroCosto Estado { get; set; }

        public FormaRegistroFuncionarioCentroCosto FormaRegistro { get; set; }

    }
}
