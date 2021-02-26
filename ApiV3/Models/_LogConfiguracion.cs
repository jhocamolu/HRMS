using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("_LogConfiguracion", Schema = "util")]
    public class _LogConfiguracion
    {

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Model { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Tabla { get; set; }

        public bool Activo { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime Fecha { get; set; }

    }
}
