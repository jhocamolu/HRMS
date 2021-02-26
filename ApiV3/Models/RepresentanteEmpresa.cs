using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("RepresentanteEmpresa")]
    public class RepresentanteEmpresa : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        [Required]
        public int CargoId { get; set; }
        public virtual Cargo Cargo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string GrupoDocumentoSlug { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime FechaInicio { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime FechaFin { get; set; }
    }
}
