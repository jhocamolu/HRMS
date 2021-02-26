using ApiV3.Infraestructura.Enumerador;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("Contrato")]
    public class Contrato : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        #region DatoContrato
        [Required]
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        [Required]
        public int TipoContratoId { get; set; }
        public virtual TipoContrato TipoContrato { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public EstadoContrato Estado { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string NumeroContrato { get; set; }

        [Required]
        public int CargoDependenciaId { get; set; }
        public virtual CargoDependencia CargoDependencia { get; set; }

        [Required]
        public int PeriodoPrueba { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime FechaInicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaFinalizacion { get; set; }

        public int? CausalTerminacionId { get; set; }
        public virtual CausalTerminacion CausalTerminacion { get; set; }

        [Column(TypeName = "text")]
        public string ObservacionFinalizacionContrato { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaTerminacion { get; set; }
        #endregion

        #region DatosLaborales
        [Required]
        [Column(TypeName = "money")]
        public double Sueldo { get; set; }

        [Required]
        public int CargoGrupoId { get; set; }
        public virtual CargoGrupo CargoGrupo { get; set; }

        [Required]
        public int CentroOperativoId { get; set; }
        public virtual CentroOperativo CentroOperativo { get; set; }

        [Required]
        public int DivisionPoliticaNivel2Id { get; set; }
        public virtual DivisionPoliticaNivel2 DivisionPoliticaNivel2 { get; set; }

        [Required]
        public int CentroCostoId { get; set; }
        public virtual CentroCosto CentroCosto { get; set; }

        [Required]
        public int FormaPagoId { get; set; }
        public virtual FormaPago FormaPago { get; set; }

        [Required]
        public int TipoMonedaId { get; set; }
        public virtual TipoMoneda TipoMoneda { get; set; }


        public int? EntidadFinancieraId { get; set; }
        public virtual EntidadFinanciera EntidadFinanciera { get; set; }

        [Required]
        public int TipoPeriodoId { get; set; }
        public virtual TipoPeriodo TipoPeriodo { get; set; }


        public int? TipoCuentaId { get; set; }
        public virtual TipoCuenta TipoCuenta { get; set; }


        [Column(TypeName = "varchar(255)")]
        public string NumeroCuenta { get; set; }
        #endregion

        #region Otros
        [Required]
        public bool RecibeDotacion { get; set; }

        [Required]
        public int JornadaLaboralId { get; set; }
        public virtual JornadaLaboral JornadaLaboral { get; set; }

        [Required]
        public bool EmpleadoConfianza { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public ProcedimientoRetenciones ProcedimientoRetencion { get; set; }

        [Column(TypeName = "text")]
        public string Observaciones { get; set; }

        [Column(TypeName = "text")]
        public string Justificacion { get; set; }

        [Required]
        public int TipoCotizanteSubtipoCotizanteId { get; set; }
        public virtual TipoCotizanteSubtipoCotizante TipoCotizanteSubtipoCotizante { get; set; }

        [Required]
        public bool ExtranjeroNoObligadoACotizarAPension { get; set; }

        [Required]
        public bool ColombianoEnElExterior { get; set; }
        #endregion


        [Required]
        public int GrupoNominaId { get; set; }
        public virtual GrupoNomina GrupoNomina { get; set; }

        public virtual ICollection<ContratoAdministradora> ContratoAdministradoras { get; set; }

        public virtual ICollection<ContratoCentroTrabajo> ContratoCentroTrabajos { get; set; }

    }
}