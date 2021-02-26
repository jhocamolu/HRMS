using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("Sustituto")]
    public class Sustituto : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CargoASustituirId { get; set; }
        public virtual Cargo CargoASustituir { get; set; }

        public int? CentroOperativoASutituirId { get; set; }
        public virtual CentroOperativo CentroOperativoASutituir { get; set; }

        [Required]
        public int CargoSustitutoId { get; set; }
        public virtual Cargo CargoSustituto { get; set; }

        public int? CentroOperativoSustitutoId { get; set; }
        public virtual CentroOperativo CentroOperativoSustituto { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime FechaInicio { get; set; }

        [Required]
        [Column(TypeName = "datetime")]
        public DateTime FechaFinal { get; set; }

        [Column(TypeName = "text")]
        public string Observacion { get; set; }

    }
}
