using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("NominaFuenteNovedad")]
    public class NominaFuenteNovedad : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Modulo { get; set; }

        [Required]
        public int ModuloRegistroId { get; set; }

        public int? DiasCausados { get; set; }

    }
}