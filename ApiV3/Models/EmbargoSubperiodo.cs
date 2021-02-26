using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("EmbargoSubperiodo")]
    public class EmbargoSubperiodo : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }


        public int EmbargoId { get; set; }
        public virtual Embargo Embargo { get; set; }


        public int SubPeriodoId { get; set; }
        public virtual SubPeriodo SubPeriodo { get; set; }
    }
}