using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("DivisionPoliticaNivel2", Schema = "dbo")]
    public class DivisionPoliticaNivel2 : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Codigo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        public int DivisionPoliticaNivel1Id { get; set; }
        public virtual DivisionPoliticaNivel1 DivisionPoliticaNivel1 { get; set; }

    }
}
