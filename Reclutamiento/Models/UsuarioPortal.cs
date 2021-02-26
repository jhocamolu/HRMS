using ApiV3.Infraestructura.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Reclutamiento.Models
{
    [Table("UsuarioPortal")]
    public class UsuarioPortal
    {

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Column(TypeName = "varchar(255)")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Column(TypeName = "varchar(255)")]
        public string Clave { get; set; }

    }
}
