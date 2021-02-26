using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("Juzgado")]
    public class Juzgado : AuditoriaRegistro
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Codigo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string NumeroCuentaJudicial { get; set; }

        public int DivisionPoliticaNivel2Id { get; set; }
        public virtual DivisionPoliticaNivel2 DivisionPoliticaNivel2 { get; set; }

    }
}
