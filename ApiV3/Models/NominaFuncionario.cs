using ApiV3.Infraestructura.Enumerador;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("NominaFuncionario")]
    public class NominaFuncionario : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int NominaId { get; set; }
        public virtual Nomina Nomina { get; set; }

        [Required]
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public double NetoPagar { get; set; }

        [Required]
        [Column(TypeName = "char(30)")]
        public EstadoNominaFuncionario Estado { get; set; }
    }
}
