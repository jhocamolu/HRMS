using ApiV3.Infraestructura.Enumerador;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("AplicacionExternaCargo")]
    public class AplicacionExternaCargo : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AplicacionExternaId { get; set; }
        public virtual AplicacionExterna AplicacionExterna { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public TipoAplicacionExternaCargo Tipo { get; set; }

        public int? CentroOperativoDependienteId { get; set; }
        public virtual CentroOperativo CentroOperativoDependiente { get; set; }

        [Required]
        public int CargoDependenciaIndependienteId { get; set; }
        public virtual CargoDependencia CargoDependenciaIndependiente { get; set; }

        public int? CentroOperativoIndependienteId { get; set; }
        public virtual CentroOperativo CentroOperativoIndependiente { get; set; }

    }
}
