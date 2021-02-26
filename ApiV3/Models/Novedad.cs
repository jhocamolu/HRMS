using ApiV3.Infraestructura.Enumerador;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("Novedad")]
    public class Novedad : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        [Required]
        public int CategoriaNovedadId { get; set; }
        public virtual CategoriaNovedad CategoriaNovedad { get; set; }

        [Required]
        [Column(TypeName = "smalldatetime")]
        public DateTime FechaAplicacion { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? FechaFinalizacion { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public UnidadMedida Unidad { get; set; }

        [Column(TypeName = "money")]
        public decimal? Valor { get; set; }

        [Column(TypeName = "decimal(16,6)")]
        public double? Cantidad { get; set; }

        public int? TerceroId { get; set; }

        [Column(TypeName = "text")]
        public string Observacion { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public EstadoNovedad Estado { get; set; }

        public virtual ICollection<NovedadSubperiodo> NovedadSubperiodos { get; set; }
    }
}
