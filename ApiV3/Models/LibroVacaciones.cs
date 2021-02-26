using ApiV3.Infraestructura.Enumerador;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("LibroVacaciones")]
    public class LibroVacacion : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ContratoId { get; set; }
        public virtual Contrato Contrato { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime InicioCausacion { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime FinCausacion { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public TipoLibroVacaciones Tipo { get; set; }

        [Required]
        [Column(TypeName = "smallint")]
        public int DiasTrabajados { get; set; }

        [Required]
        [Column(TypeName = "tinyint")]
        public int DiasCausados { get; set; }

        [Required]
        [Column(TypeName = "float")]
        public float DiasDisponibles { get; set; }

        [Column(TypeName = "float")]
        public float? DiasDebe { get; set; }

        public int FuncionarioId { get; set; }

    }
}
