using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("CargoGrupo")]
    public class CargoGrupo : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CargoId { get; set; }
        public virtual Cargo Cargo { get; set; }

        [Required]
        public int GrupoId { get; set; }
        public virtual Grupo Grupo { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool Defecto { get; set; }

    }
}
