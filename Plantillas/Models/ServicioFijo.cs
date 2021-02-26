using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plantillas.Models
{
    [Table("ServicioFijo")]
    public class ServicioFijo : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(1000)]
        public string Servicio { get; set; }
    }
}
