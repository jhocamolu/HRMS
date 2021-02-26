using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("ProrrogaAusentismo")]
    public class ProrrogaAusentismo : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        public int ProrrogaId { get; set; }
        public virtual AusentismoFuncionario Prorroga { get; set; }

        public int AusentismoId { get; set; }
        public virtual AusentismoFuncionario Ausentismo { get; set; }

    }
}
