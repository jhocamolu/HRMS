using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiV3.Models
{
    [Table("ActividadFuncionarioCentroCosto")]
    public class ActividadFuncionarioCentroCosto : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ActividadFuncionarioId { get; set; }
        public virtual ActividadFuncionario ActividadFuncionario { get; set; }

        [Required]
        public int FuncionarioCentroCostoId { get; set; }
        public virtual FuncionarioCentroCosto FuncionarioCentroCosto { get; set; }

    }
}
