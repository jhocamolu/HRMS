using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ayuda.Models
{
    [Table("Clave")]
    public class Clave
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Palabra { get; set; }

        public ICollection<ArticuloClave> ArticuloClaves { get; set; }
    }
}
