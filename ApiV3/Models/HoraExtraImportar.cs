using ApiV3.Infraestructura.Enumerador;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("HoraExtraImportar")]
    public class HoraExtraImportar : AuditoriaRegistro
    {
        [Key]
        [Required]
        [Display(Description = "Identificador único del registro en la tabla")]
        public int Id { get; set; }

        [Required]
        [Display(Description = "Identificador unico para la importacion de horas extras")]
        [Column(TypeName = "varchar(255)")]
        public string OrigenId { get; set; }

        [Required]
        [Display(Description = "Numero documento identificacion funcionario.")]
        public string FuncionarioDocumento { get; set; }

        
        [Display(Description = "Numero documento identificacion funcionario.")]
        public int? FuncionarioId { get; set; }

        [Required]
        [Display(Description = "Fecha en la que se realiza la hora extra.")]
        public DateTime Fecha { get; set; }

        [Required]
        [Display(Description = "Identificador del tipo de hora extra.")]
        public string Tipo { get; set; }
        
        
        [Display(Description = "Identificador del tipo de hora extra.")]
        public string TipoHoraExtra { get; set; }

        [Required]
        [Display(Description = "Cantidad de horas extas registradas.")]
        [Column(TypeName = "varchar(255)")]
        public string Cantidad { get; set; }

        [Required]
        [Display(Description = "Estado de la hora extra.")]
        [Column(TypeName = "varchar(255)")]
        public EstadoHoraExtra? Estado { get; set; }

        [Required]
        [Display(Description = "Valor de las horas extras registradas.")]
        [Column(TypeName = "money")]
        public decimal? Valor { get; set; }

        [Required]
        [Display(Description = "Observacion de la hora extra.")]
        [Column(TypeName = "text")]
        public string Observacion { get; set; }
    }
}
