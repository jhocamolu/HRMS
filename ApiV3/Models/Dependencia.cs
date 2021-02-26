using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("Dependencia")]
    public class Dependencia : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Codigo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        public virtual ICollection<DependenciaJerarquia> SoyPadreDe { get; set; }

        public virtual ICollection<DependenciaJerarquia> SoyHijoDe { get; set; }

    }
}
