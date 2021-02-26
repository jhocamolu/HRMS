using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("DependenciaJerarquia")]
    public class DependenciaJerarquia : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        public int DependenciaHijoId { get; set; }
        public virtual Dependencia DependenciaHijo { get; set; }


        public int? DependenciaPadreId { get; set; }
        public virtual Dependencia DependenciaPadre { get; set; }

    }
}
