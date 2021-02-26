using Plantillas.Dominio.Enumerador;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plantillas.Models
{
    public abstract class AuditoriaRegistro
    {
        //[Required]
        [Column(TypeName = "varchar(10)")]
        public EstadoRegistro? EstadoRegistro { get; set; }

        //[Required]
        [Column(TypeName = "varchar(255)")]
        public string CreadoPor { get; set; }

        //[Required]
        public DateTime? FechaCreacion { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string ModificadoPor { get; set; }

        public DateTime? FechaModificacion { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string EliminadoPor { get; set; }

        public DateTime? FechaEliminacion { get; set; }

    }
}
