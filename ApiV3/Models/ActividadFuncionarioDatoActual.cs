using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiV3.Models
{
    [Table("VW_FuncionarioDatoActual")]
    public class ActividadFuncionarioDatoActual
    {
        [Key]
        public int Id { get; set; }

        public int? FuncionarioId { get; set; }
        public virtual FuncionarioDatoActual Funcionario { get; set; }

        public string Nombre { get; set; }

        public int CargoId { get; set; }

        public string Cargo { get; set; }

        public int DependenciaId { get; set; }
        
        public string Dependencia { get; set; }
    }
}
