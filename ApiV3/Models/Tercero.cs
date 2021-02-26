using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("Tercero")]
    public class Tercero : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nit { get; set; }

        [Required]
        [Column(TypeName = "smallint")]
        public int DigitoVerificacion { get; set; }

        [Required]
        public int DivisionPoliticaNivel2Id { get; set; }
        public virtual DivisionPoliticaNivel2 DivisionPoliticaNivel2 { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Telefono { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Direccion { get; set; }

        [Required]
        public int EntidadFinancieraId { get; set; }
        public virtual EntidadFinanciera EntidadFinanciera { get; set; }

        [Required]
        public int TipoCuentaId { get; set; }
        public virtual TipoCuenta TipoCuenta { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string NumeroCuenta { get; set; }

        [Column(TypeName = "text")]
        public string Descripcion { get; set; }

    }
}
