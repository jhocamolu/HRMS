using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("DeduccionRetefuente")]
    public class DeduccionRetefuente : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        [Required]
        public int AnnoVigenciaId { get; set; }
        public virtual AnnoVigencia AnnoVigencia { get; set; }


        [Column(TypeName = "money")]
        public double? InteresVivienda { get; set; }


        [Column(TypeName = "money")]
        public double? MedicinaPrepagada { get; set; }

    }
}
