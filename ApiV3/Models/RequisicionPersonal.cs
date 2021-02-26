using ApiV3.Infraestructura.Enumerador;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("RequisicionPersonal", Schema = "dbo")]
    public class RequisicionPersonal : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        #region Info Solicitante
        [Required]
        public int CargoDependenciaSolicitanteId { get; set; }
        public virtual CargoDependencia CargoDependenciaSolicitante { get; set; }

        public int? CentroOperativoSolicitanteId { get; set; }
        public virtual CentroOperativo CentroOperativoSolicitante { get; set; }

        [Required]
        public int FuncionarioSolicitanteId { get; set; }
        public virtual Funcionario FuncionarioSolicitante { get; set; }
        #endregion

        #region Info Cargo Solicitado
        [Required]
        public int CargoDependenciaSolicitadoId { get; set; }
        public virtual CargoDependencia CargoDependenciaSolicitado { get; set; }

        public int? CentroOperativoSolicitadoId { get; set; }
        public virtual CentroOperativo CentroOperativoSolicitado { get; set; }

        [Required]
        public int DivisionPoliticaNivel2Id { get; set; }
        public virtual DivisionPoliticaNivel2 DivisionPoliticaNivel2 { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public int TipoContratoId { get; set; }
        public virtual TipoContrato TipoContrato { get; set; }

        [Required]
        public int CentroCostoId { get; set; }
        public virtual CentroCosto CentroCosto { get; set; }
        #endregion

        #region TiempoContratacion
        [Required]
        [Column(TypeName = "date")]
        public DateTime FechaInicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaFin { get; set; }
        #endregion

        #region  Info vacante

        [Required]
        public int MotivoVacanteId { get; set; }
        public virtual MotivoVacante MotivoVacante { get; set; }


        public int? FuncionarioAQuienReemplazaId { get; set; }
        public virtual Funcionario FuncionarioAQuienReemplaza { get; set; }
        #endregion

        #region Pefil y Competencias del cargo
        [Required]
        [Column(TypeName = "text")]
        public string PerfilCargo { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string CompetenciaCargo { get; set; }

        public TipoReclutamiento? TipoReclutamiento { get; set; }

        [Column(TypeName = "money")]
        public double? Salario { get; set; }

        public bool? SalarioPortalReclutamiento { get; set; }

        public bool? PerfilPortalReclutamiento { get; set; }

        public bool? CompetenciaPortalReclutamiento { get; set; }

        [Column(TypeName = "text")]
        public string Observacion { get; set; }

        [Column(TypeName = "text")]
        public string Justificacion { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public EstadoRequisicionPersonal Estado { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaAutorizacion { get; set; }
        #endregion
    }
}
