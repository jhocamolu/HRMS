using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ApiV3.Models
{
    [Table("Actividad")]
    public class Actividad : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Codigo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Nombre { get; set; }

        [Required]
        public int PromedioProductividad { get; set; }

        [Required]
        [Column(TypeName = "decimal(19, 6)")]
        public decimal ValorComplejidad { get; set; }

        [Column(TypeName = "text")]
        public string Descripcion { get; set; }

    }
}
