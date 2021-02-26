using ApiV3.Infraestructura.Enumerador;
using ApiV3.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Reclutamiento.Models
{
    
    public class UsuarioAplicacion : IdentityUser
    {
        [Required]
        public int HojaDeVidaId { get; set; }
        public virtual HojaDeVida HojaDeVida { get; set; }
       
        [Required]
        [Column(TypeName = "text")]
        public string TokenGhestic { get; set; }
    }
}
