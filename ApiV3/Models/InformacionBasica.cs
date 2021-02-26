using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("InformacionBasica", Schema = "dbo")]
    public class InformacionBasica : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        #region BASICOS        
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nit { get; set; }

        [Column(TypeName = "varchar(255)")]
        public int DigitoVerificacion { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string RazonSocial { get; set; }

        [Required]
        public int ActividadEconomicaId { get; set; }
        public virtual ActividadEconomica ActividadEconomica { get; set; }
        #endregion

        #region LOCALIZACION
        [Required]
        public int DivisionPoliticaNivel2Id { get; set; }
        public virtual DivisionPoliticaNivel2 DivisionPoliticaNivel2 { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Direccion { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Telefono { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Fax { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string CorreoElectronico { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Web { get; set; }
        #endregion

        #region EMPRESA
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaConstitucion { get; set; }

        public int TipoContribuyenteId { get; set; }
        public virtual TipoContribuyente TipoContribuyente { get; set; }


        public int OperadorPagoId { get; set; }
        public virtual OperadorPago OperadorPago { get; set; }


        public int ArlId { get; set; }
        public virtual Administradora Arl { get; set; }

        [Required]
        public int TipoDocumentoId { get; set; }
        public virtual TipoDocumento TipoDocumento { get; set; }

        [Required]
        public int NaturalezaJuridicaId { get; set; }
        public virtual NaturalezaJuridica NaturalezaJuridica { get; set; }

        [Required]
        public int TipoPersonaId { get; set; }
        public virtual TipoPersona TipoPersona { get; set; }

        [Required]
        public int ClaseAportanteTipoAportanteId { get; set; }
        public virtual ClaseAportanteTipoAportante ClaseAportanteTipoAportante { get; set; }

        [Required]
        public int CargoId { get; set; }
        public virtual Cargo Cargo { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool BeneficiarioLey1429De2010 { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool BeneficiarioImpuestoEquidad { get; set; }
        #endregion
    }
}
