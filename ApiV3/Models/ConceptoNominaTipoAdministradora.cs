using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiV3.Models
{
    [Table("ConceptoNominaTipoAdministradora")]
    public class ConceptoNominaTipoAdministradora : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ConceptoNominaId { get; set; }
        public virtual ConceptoNomina ConceptoNomina { get; set; }

        [Required]
        public int TipoAdministradoraId { get; set; }
        public virtual TipoAdministradora TipoAdministradora { get; set; }
    }
}
