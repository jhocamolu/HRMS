using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("ActividadCentroCosto")]
    public class ActividadCentroCosto : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ActividadId { get; set; }
        public virtual Actividad Actividad { get; set; }

        [Required]
        public int CentroCostoId { get; set; }
        public virtual CentroCosto CentroCosto { get; set; }

        [Required]
        public int MunicipioId { get; set; }
        public virtual DivisionPoliticaNivel2 Municipio { get; set; }

    }
}
