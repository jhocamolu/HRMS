using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("EntidadFinanciera")]
    public class EntidadFinanciera : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Codigo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nit { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Dv { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

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
        [Column(TypeName = "varchar(255)")]
        public string RepresentanteLegal { get; set; }

        public bool? EntidadPorDefecto { get; set; }
    }
}
