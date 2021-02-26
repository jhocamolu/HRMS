using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ayuda.Models
{
    [Table("Categoria")]
    public class Categoria : AuditoriaRegistro
    {
        [Key]

        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int Orden { get; set; }

        public int? CategoriaId { get; set; }
        public virtual Categoria Padre { get; set; }

        public virtual ICollection<Categoria> Categorias { get; set; }

    }
}
