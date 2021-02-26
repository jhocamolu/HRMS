using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("BeneficioAdjunto")]
    public class BeneficioAdjunto : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }


        public int BeneficioId { get; set; }
        public virtual Beneficio Beneficio { get; set; }


        public int TipoBeneficioRequisitoId { get; set; }
        public virtual TipoBeneficioRequisito TipoBeneficioRequisito { get; set; }


        [Column(TypeName = "varchar(100)")]
        public string Adjunto { get; set; }
    }
}
