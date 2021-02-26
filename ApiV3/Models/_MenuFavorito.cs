using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("_MenuFavorito", Schema = "util")]
    public class _MenuFavorito : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FuncionarioId { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string ItemMenu { get; set; }
    }
}
