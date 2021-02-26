using Ayuda.Dominio.Enumerador;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ayuda.Models
{
    public abstract class AuditoriaRegistro
    {
        [Required]
        public EstadoRegistro EstadoRegistro { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }
        [Required]
        public DateTime FechaModificacion { get; set; }
        public DateTime? FechaEliminacion { get; set; }

    }
}
