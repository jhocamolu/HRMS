using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("FuncionVariable")]
    public class FuncionVariable : AuditoriaRegistro
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public int FuncionNominaId { get; set; }
        public virtual FuncionNomina FuncionNomina { get; set; }

        [Required]
        public int VariableNominaId { get; set; }
        public virtual VariableNomina VariableNomina { get; set; }

        [Required]
        public int Orden { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string ValorDefecto { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string NombreParametro { get; set; }
    }
}
