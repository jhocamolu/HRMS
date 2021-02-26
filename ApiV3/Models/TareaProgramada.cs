using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("TareaProgramada", Schema = "dbo")]
    public class TareaProgramada : AuditoriaRegistro
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Column(TypeName = "varchar(255)")]
        [Display(Description = "Nombre de la notificación.")]
        public string Nombre { get; set; }

        [Column(TypeName = "varchar(255)")]
        [Display(Description = "Alias unico para ejecuatr la notificación.")]
        public string Alias { get; set; }

        [Column(TypeName = "varchar(255)")]
        [Display(Description = "Especifica la frecuencia en la que se ejecuta la tarea programada.")]
        public string Periodicidad { get; set; }

        [Column(TypeName = "text")]
        [Display(Description = "Breve explicación del uso y funcionamiento de la tarea programada.")]
        public string Descripcion { get; set; }

        [Column(TypeName = "varchar(255)")]
        [Display(Description = "Breve descripción del proceso que realiza la tarea.")]
        public string Instruccion { get; set; }

        [Display(Description = "Indica si la tarea se va a iniciar.")]
        public bool EnEjecucion { get; set; }

        //public DateTime UltimaEjecucion { get; set; }
    }
}