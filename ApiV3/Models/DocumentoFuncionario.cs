using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("DocumentoFuncionario")]
    public class DocumentoFuncionario : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        public int TipoSoporteId { get; set; }
        public virtual TipoSoporte TipoSoporte { get; set; }

        [Column(TypeName = "text")]
        public string Comentario { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Adjunto { get; set; }
    }
}
