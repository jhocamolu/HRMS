using ApiV3.Infraestructura.Enumerador;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiV3.Models
{
    [Table("ActividadFuncionario")]
    public class ActividadFuncionario : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        [Required]
        public int ActividadId { get; set; }
        public virtual Actividad Actividad { get; set; }

        [Required]
        public int MunicipioId { get; set; }
        public virtual DivisionPoliticaNivel2 Municipio { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime FechaInicio { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime? FechaFinalizacion { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public EstadoActividadFuncionario Estado { get; set; }
    }
}
