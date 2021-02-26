using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("FormaPago")]
    public class FormaPago : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        public bool HabilitaEnContrato { get; set; }

        [Required]
        public bool HabilitaEntidadFinanciera { get; set; }
    }
}
