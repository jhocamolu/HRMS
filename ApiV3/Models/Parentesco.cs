using ApiV3.Infraestructura.Enumerador;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("Parentesco")]
    public class Parentesco : AuditoriaRegistro
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public TipoParentescos Tipo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public GradoParentescos Grado { get; set; }

        public int NumeroPersonasPermitidas { get; set; }
    }
}
