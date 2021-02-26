using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiV3.Models
{
    [Table("VW_ContratoCentroTrabajoCambios")]
    public class ContratoCentroTrabajoCambio
    {
        [Key]
        public int Id { get; set; }

        public int ContratoId { get; set; }
        public virtual Contrato Contrato { get; set; }

        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        public string Anterior { get; set; }

        public string Actual { get; set; }

        public int centroTrabajoActualId { get; set; }
        public virtual CentroTrabajo centroTrabajoActual { get; set; }

        public DateTime? FechaInicio { get; set; }

        [Column(TypeName = "text")]
        public string Observacion { get; set; }

    }
}
