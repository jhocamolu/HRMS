using ApiV3.Infraestructura.Enumerador;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("GastoViaje")]
    public class GastoViaje : AuditoriaRegistro
    {
        [Key]
        [Display(Description = "Identificador único del registro en la tabla.")]
        public int Id { get; set; }

        [Required]
        [Display(Description = "Identificador del funcionario.")]
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        [Required]
        [Display(Description = "Identificador del tipo de gasto de viaje.")]
        public int TipoGastoViajeId { get; set; }
        public virtual TipoGastoViaje TipoGastoViaje { get; set; }

        [Required]
        [Display(Description = "Fecha en la que se registra la hora extra.")]
        public DateTime Fecha { get; set; }

        [Required]
        [Display(Description = "Valor de las horas extras registradas.")]
        [Column(TypeName = "money")]
        public double Valor { get; set; }

        [Required]
        [Display(Description = "Estados que puede tener el gasto de viaje.")]
        public EstadoGastoViaje Estado { get; set; }
    }
}
