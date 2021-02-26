using ApiV3.Infraestructura.Enumerador;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("Notificacion", Schema = "dbo")]
    public class Notificacion : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }


        [Required]
        [Column(TypeName = "varchar(255)")]
        public TipoNotificacion Tipo { get; set; }


        [Required]
        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }



        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Titulo { get; set; }


        [Required]
        [Column(TypeName = "text")]
        public string Mensaje { get; set; }


        [Required]
        public bool EnEjecucion { get; set; }

        public int Pendiente { get; set; }

        public int Enviado { get; set; }

        public int Fallido { get; set; }
    }
}
