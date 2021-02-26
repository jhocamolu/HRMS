
using ApiV3.Infraestructura.Enumerador;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Reclutamiento.Models
{
    public abstract class AuditoriaRegistro
    {
        //[Required]
        [Column(TypeName = "char(10)")]
        public EstadoRegistro? EstadoRegistro { get; set; }

        //[Required]
        [Column(TypeName = "varchar(255)")]
        public string CreadoPor { get; set; }

        //[Required]
        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaCreacion { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string ModificadoPor { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaModificacion { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string EliminadoPor { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaEliminacion { get; set; }

    }
}
