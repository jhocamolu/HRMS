using ApiV3.Infraestructura.Enumerador;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    /// <summary>
    /// Esta tabla se soncronizara con los datos de SOFTLAND
    /// </summary>
    [Table("CuentaContable")]
    public class CuentaContable : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Cuenta { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public NaturalezaContable Naturaleza { get; set; }
    }
}
