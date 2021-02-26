using ApiV3.Infraestructura.Enumerador;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("InformacionFamiliar")]
    public class InformacionFamiliar : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

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

        [Required]
        public int ParentescoId { get; set; }
        public virtual Parentesco Parentesco { get; set; }

        [Required]
        public bool Dependiente { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public int TipoDocumentoId { get; set; }
        public virtual TipoDocumento TipoDocumento { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string NumeroDocumento { get; set; }

        public int? NivelEducativoId { get; set; }
        public virtual NivelEducativo NivelEducativo { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string TelefonoFijo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Celular { get; set; }

        [Required]
        public int DivisionPoliticaNivel2Id { get; set; }
        public virtual DivisionPoliticaNivel2 DivisionPoliticaNivel2 { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Direccion { get; set; }

        public EstadoInformacionFuncionario Estado { get; set; }

        public string Justificacion { get; set; }
    }
}
