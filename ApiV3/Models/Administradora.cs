using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("Administradora")]
    public class Administradora : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Codigo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nit { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Dv { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        public int TipoAdministradoraId { get; set; }
        public virtual TipoAdministradora TipoAdministradora { get; set; }

    }
}
