using ApiV3.Infraestructura.Enumerador;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ApiV3.Models
{
    [Table("NominaDetalle")]
    public class NominaDetalle : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int NominaFuncionarioId { get; set; }
        public virtual NominaFuncionario NominaFuncionario { get; set; }

        public int? NominaFuenteNovedadId { get; set; }
        public virtual NominaFuenteNovedad NominaFuenteNovedad { get; set; }

        [Required]
        public int ConceptoNominaId { get; set; }
        public virtual ConceptoNomina ConceptoNomina { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string UnidadMedida { get; set; }

        [Required]
        [Column(TypeName = "decimal(16,6)")]
        public double Cantidad { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public double Valor { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public EstadoNominaDetalle Estado { get; set; }

        [Column(TypeName = "text")]
        public string Inconsistencia { get; set; }

        [Column(TypeName = "text")]
        public string Observacion { get; set; }

        public virtual IQueryable<bool> ValorEditable { get; set; }

    }
}
