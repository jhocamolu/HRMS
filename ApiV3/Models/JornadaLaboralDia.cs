using ApiV3.Infraestructura.Enumerador;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("JornadaLaboralDia")]
    public class JornadaLaboralDia : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        public int JornadaLaboralId { get; set; }
        public virtual JornadaLaboral JornadaLaboral { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public DiaSemana Dia { get; set; }

        [Required]
        [Column(TypeName = "time")]
        public TimeSpan HoraInicioJornada { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan? HoraInicioDescanso { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan? HoraFinDescanso { get; set; }

        [Required]
        [Column(TypeName = "time")]
        public TimeSpan HoraFinJornada { get; set; }
    }
}
