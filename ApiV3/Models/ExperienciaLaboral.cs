using ApiV3.Infraestructura.Enumerador;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("ExperienciaLaboral")]
    public class ExperienciaLaboral : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string NombreCargo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string NombreEmpresa { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Telefono { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Salario { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string NombreJefeInmediato { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime FechaInicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaFin { get; set; }

        [Column(TypeName = "text")]
        public string FuncionesCargo { get; set; }

        public bool? TrabajaActualmente { get; set; }

        [Column(TypeName = "text")]
        public string MotivoRetiro { get; set; }

        [Column(TypeName = "text")]
        public string Observaciones { get; set; }


        public EstadoInformacionFuncionario Estado { get; set; }

        [Column(TypeName = "text")]
        public string Justificacion { get; set; }
    }
}
