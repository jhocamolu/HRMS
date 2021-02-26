using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("HojaDeVida", Schema = "dbo")]
    public class HojaDeVida : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        #region DatosBasicos        
        [Required]
        [Column(TypeName = "varchar(255)")]
        public string PrimerNombre { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string SegundoNombre { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string PrimerApellido { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string SegundoApellido { get; set; }

        [Required]
        public int SexoId { get; set; }
        public virtual Sexo Sexo { get; set; }

        public int? EstadoCivilId { get; set; }
        public virtual EstadoCivil EstadoCivil { get; set; }

        public int? OcupacionId { get; set; }
        public virtual Ocupacion Ocupacion { get; set; }

        public bool? Pensionado { get; set; }
        #endregion

        #region Nacimiento
        [Column(TypeName = "date")]
        public DateTime? FechaNacimiento { get; set; }

        public int? DivisionPoliticaNivel2OrigenId { get; set; }
        public virtual DivisionPoliticaNivel2 DivisionPoliticaNivel2Origen { get; set; }
        #endregion

        #region Identificacion
        [Required]
        public int TipoDocumentoId { get; set; }
        public virtual TipoDocumento TipoDocumento { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string NumeroDocumento { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaExpedicionDocumento { get; set; }

        public int? DivisionPoliticaNivel2ExpedicionDocumentoId { get; set; }
        public virtual DivisionPoliticaNivel2 DivisionPoliticaNivel2ExpedicionDocumento { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Nit { get; set; }

        public int? DigitoVerificacion { get; set; }
        #endregion

        #region Residencia
        public int? DivisionPoliticaNivel2ResidenciaId { get; set; }
        public virtual DivisionPoliticaNivel2 DivisionPoliticaNivel2Residencia { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Celular { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string TelefonoFijo { get; set; }

        public int? TipoViviendaId { get; set; }
        public virtual TipoVivienda TipoVivienda { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Direccion { get; set; }
        #endregion

        #region LibretaMilitar
        public int? ClaseLibretaMilitarId { get; set; }
        public virtual ClaseLibretaMilitar ClaseLibretaMilitar { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string NumeroLibreta { get; set; }

        public int? Distrito { get; set; }
        #endregion

        #region LicenciaConduccion
        public int? LicenciaConduccionAId { get; set; }
        public virtual LicenciaConduccion LicenciaConduccionA { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LicenciaConduccionAFechaVencimiento { get; set; }

        public int? LicenciaConduccionBId { get; set; }
        public virtual LicenciaConduccion LicenciaConduccionB { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LicenciaConduccionBFechaVencimiento { get; set; }

        public int? LicenciaConduccionCId { get; set; }
        public virtual LicenciaConduccion LicenciaConduccionC { get; set; }

        [Column(TypeName = "date")]
        public DateTime? LicenciaConduccionCFechaVencimiento { get; set; }
        #endregion

        #region Otros
        [Column(TypeName = "varchar(255)")]
        public string TallaCamisa { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string TallaPantalon { get; set; }

        public double? NumeroCalzado { get; set; }

        public bool? UsaLentes { get; set; }

        public int? TipoSangreId { get; set; }
        public virtual TipoSangre TipoSangre { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string CorreoElectronicoPersonal { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Adjunto { get; set; }
        #endregion

        public int? Edad
        {
            get
            {
                if (FechaNacimiento != null)
                {
                    var nacimiento = (DateTime)FechaNacimiento;
                    return DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
                }
                else return 0;
            }
            set { }
        }
    }
}
