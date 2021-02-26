using ApiV3.Infraestructura.Enumerador;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("HoraExtra")]
    public class HoraExtra : AuditoriaRegistro
    {
        [Key]
        [Required]
        [Display(Description = "Identificador único del registro en la tabla")]
        public int Id { get; set; }

        [Required]
        [Display(Description = "Código que identifica el funcionario.")]
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        [Required]
        [Display(Description = "Identificador del tipo de hora extra.")]
        public int TipoHoraExtraId { get; set; }
        public virtual TipoHoraExtra TipoHoraExtra { get; set; }

        [Required]
        [Display(Description = "Fecha en la que se realiza la hora extra.")]
        public DateTime Fecha { get; set; }

        [Required]
        [Display(Description = "Cantidad de horas extas registradas.")]
        [Column(TypeName = "varchar(255)")]
        public string Cantidad { get; set; }

        [Display(Description = "Valor de las horas extras registradas.")]
        [Column(TypeName = "money")]
        public double? Valor { get; set; }

        [Required]
        [Display(Description = "Procedencia del registro ingresado, ya sea ingresado de forma manual o automática desde el aplicativo de horas extras.")]
        [Column(TypeName = "varchar(255)")]
        public FormaRegistroHoraExtra FormaRegistro { get; set; }

        [Required]
        [Display(Description = "Estado de la hora extra.")]
        [Column(TypeName = "varchar(255)")]
        public EstadoHoraExtra Estado { get; set; }

        [Required]
        [Display(Description = "Identificador unico para la importacion de horas extras")]
        [Column(TypeName = "varchar(255)")]
        public string OrigenId { get; set; }

        [Required]
        [Display(Description = "Observacion de la hora extra.")]
        [Column(TypeName = "text")]
        public string Observacion { get; set; }
    }
}
