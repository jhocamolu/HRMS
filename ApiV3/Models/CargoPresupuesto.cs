using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("CargoPresupuesto")]
    public class CargoPresupuesto : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CargoId { get; set; }
        public virtual Cargo Cargo { get; set; }

        [Required]
        public int AnnoVigenciaId { get; set; }
        public virtual AnnoVigencia AnnoVigencia { get; set; }

        [Required]
        public int Cantidad { get; set; }
    }
}
