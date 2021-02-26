
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Utilidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace ApiV3.Models
{
    [Table("AusentismoFuncionario")]
    public class AusentismoFuncionario : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        [Required]
        public int TipoAusentismoId { get; set; }
        public virtual TipoAusentismo TipoAusentismo { get; set; }

        public int? DiagnosticoCieId { get; set; }
        public virtual DiagnosticoCie DiagnosticoCie { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime FechaInicio { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime FechaFin { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaIniciaReal { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaFinalReal { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan? HoraInicio { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan? HoraFin { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string NumeroIncapacidad { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Adjunto { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public EstadoAusentismo Estado { get; set; }

        [Column(TypeName = "text")]
        public string Justificacion { get; set; }

        [Column(TypeName = "text")]
        public string Observacion { get; set; }

        public int? NumeroDeDias
        {
            get
            {
                if (FechaInicio != null && FechaFin != null)
                {
                    var calcularDias = (FechaFin - FechaInicio).TotalDays;
                    if (FechaFin != FechaInicio)
                    {
                        calcularDias = calcularDias + 1;
                    }
                    return (int)calcularDias;
                }
                else return 0;
            }
            set { }
        }

        public bool ValidaFechaFinal { get; set; }

        public bool ValidaTodo { get; set; }

        [IgnoreDataMember]
        public virtual ICollection<ProrrogaAusentismo> ProrrogaDe { get; set; }

        public virtual ICollection<ProrrogaAusentismo> AusentismoDe { get; set; }
    }
}
