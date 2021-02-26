using ApiV3.Infraestructura.Enumerador;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("Embargo")]
    public class Embargo : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        public int? JuzgadoId { get; set; }
        public virtual Juzgado Juzgado { get; set; }

        [Required]
        public int TipoEmbargoId { get; set; }
        public virtual TipoEmbargo TipoEmbargo { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string NumeroProceso { get; set; }

        [Column(TypeName = "money")]
        public double? ValorEmbargo { get; set; }

        [Column(TypeName = "money")]
        public double? ValorCuota { get; set; }

        [Required]
        public int Prioridad { get; set; }

        [Required]
        public int EntidadFinancieraId { get; set; }
        public virtual EntidadFinanciera EntidadFinanciera { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string NumeroCuenta { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string NumeroDocumentoDemandante { get; set; }

        [Column(TypeName = "smallint")]
        public int? DigitoVerificacionDemandante { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Demandante { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaInicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaFin { get; set; }

        [Required]
        [Column(TypeName = "varchar(10)")]
        public EstadoEmbargo Estado { get; set; }

        /// Indica si se insertó un valor de prioridad que ya existía e hizo actualizar las demás
        [Column(TypeName = "bit")]
        public bool? ActualizaPrioridad { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal? PorcentajeCuota { get; set; }

        [Column(TypeName = "text")]
        public string Justificacion { get; set; }

        public virtual ICollection<EmbargoSubperiodo> EmbargoSubperiodos { get; set; }
    }
}