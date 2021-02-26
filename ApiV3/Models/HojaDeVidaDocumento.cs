using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("HojaDeVidaDocumento", Schema = "dbo")]
    public class HojaDeVidaDocumento : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        public int HojaDeVidaId { get; set; }
        public virtual HojaDeVida HojaDeVida { get; set; }

        public int TipoSoporteId { get; set; }
        public virtual TipoSoporte TipoSoporte { get; set; }

        [Column(TypeName = "text")]
        public string Comentario { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string Adjunto { get; set; }
    }
}
