using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiV3.Models
{
    [Table("NominaCentroCosto")]
    public class NominaCentroCosto : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int NominaFuncionarioId { get; set; }
        public virtual NominaFuncionario NominaFuncionario { get; set; }

        [Required]
        public int ConceptoNominaId { get; set; }
        public virtual ConceptoNomina ConceptoNomina { get; set; }

        [Required]
        public int CentroCostoId { get; set; }
        public virtual CentroCosto CentroCosto { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string NitTercero { get; set; }

        [Column(TypeName = "money")]
        public double? Valor { get; set; }

    }
}
