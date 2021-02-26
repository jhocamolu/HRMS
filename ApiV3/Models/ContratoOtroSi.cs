using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("ContratoOtroSi")]
    public class ContratoOtroSi : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        public int ContratoId { get; set; }
        public virtual Contrato Contrato { get; set; }

        public int TipoContratoId { get; set; }
        public virtual TipoContrato TipoContrato { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaFinalizacion { get; set; }

        public int CargoDependenciaId { get; set; }
        public virtual CargoDependencia CargoDependencia { get; set; }

        public int NumeroOtroSi { get; set; }

        [Column(TypeName = "money")]
        public double Sueldo { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime FechaAplicacion { get; set; }

        public int CentroOperativoId { get; set; }
        public virtual CentroOperativo CentroOperativo { get; set; }

        public int DivisionPoliticaNivel2Id { get; set; }
        public virtual DivisionPoliticaNivel2 DivisionPoliticaNivel2 { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Observaciones { get; set; }

        public bool Prorroga { get; set; }

        public int? NumeroProrroga { get; set; }

        public int? TipoOtroSiId { get; set; }
        public virtual TipoOtroSi TipoOtroSi { get; set; }
    }
}
