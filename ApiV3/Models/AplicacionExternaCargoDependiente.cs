using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("AplicacionExternaCargoDependiente")]
    public class AplicacionExternaCargoDependiente : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CargoDependenciaId { get; set; }
        public virtual CargoDependencia CargoDependencia { get; set; }

        [Required]
        public int AplicacionExternaCargoId { get; set; }
        public virtual AplicacionExternaCargo AplicacionExternaCargo { get; set; }

    }
}
