using ApiV3.Infraestructura.Enumerador;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("Libranza")]
    public class Libranza : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        [Required]
        public int EntidadFinancieraId { get; set; }
        public virtual EntidadFinanciera EntidadFinanciera { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime FechaInicio { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public double ValorPrestamo { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public EstadoLibranza Estado { get; set; }

        public int? NumeroCuotas { get; set; }

        [Column(TypeName = "text")]
        public string Observacion { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public double ValorCuota { get; set; }

        [Column(TypeName = "text")]
        public string Justificacion { get; set; }

        public virtual ICollection<LibranzaSubperiodo> LibranzaSubperiodos { get; set; }
    }
}
