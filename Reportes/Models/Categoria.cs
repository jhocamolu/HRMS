using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Reportes.Models
{
    [Table("Categoria")]
    public class Categoria : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Codigo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Alias { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }
    }
}
