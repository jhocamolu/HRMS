using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("ContratoAdministradora")]
    public class ContratoAdministradora : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ContratoId { get; set; }
        public virtual Contrato Contrato { get; set; }

        [Required]
        public int AdministradoraId { get; set; }
        public virtual Administradora Administradora { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaInicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaFin { get; set; }


        [Column(TypeName = "text")]
        public string Observacion { get; set; }
    }
}
