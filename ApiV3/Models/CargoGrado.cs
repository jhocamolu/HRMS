using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("CargoGrado")]
    public class CargoGrado : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Descripcion { get; set; }

        public int CargoId { get; set; }
        public virtual Cargo Cargo { get; set; }
    }
}
