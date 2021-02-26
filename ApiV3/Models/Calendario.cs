using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ApiV3.Models
{
    [Table("Calendario")]
    public class Calendario : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }

        [Required]
        [Column(TypeName = "text")]
        public string Nombre { get; set; }
    }
}
