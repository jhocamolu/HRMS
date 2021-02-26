using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiV3.Models
{
    [Table("TipoOtroSi")]
    public class TipoOtroSi : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        public bool Numeracion { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string DocumentoSlug { get; set; }
    }
}
