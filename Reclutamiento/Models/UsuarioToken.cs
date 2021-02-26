using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Reclutamiento.Models
{
    [Table("UsuarioToken")]
    public class UsuarioToken 
    {

        [Required]
        [Column(TypeName = "varchar(500)")]
        public string Token { get; set; }

        //[Required]        
        //public DateTime Expiracion { get; set; }

    }
}
