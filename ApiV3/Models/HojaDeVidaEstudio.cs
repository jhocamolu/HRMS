using ApiV3.Infraestructura.Enumerador;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("HojaDeVidaEstudio", Schema = "dbo")]
    public class HojaDeVidaEstudio : AuditoriaRegistro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int HojaDeVidaId { get; set; }
        public virtual HojaDeVida HojaDeVida { get; set; }

        [Required]
        public int NivelEducativoId { get; set; }
        public virtual NivelEducativo NivelEducativo { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string InstitucionEducativa { get; set; }

        [Required]
        public int PaisId { get; set; }
        public virtual Pais Pais { get; set; }

        public int? ProfesionId { get; set; }
        public virtual Profesion Profesion { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime FechaInicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaFin { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public EstadoEstudio EstadoEstudio { get; set; }

        [Column(TypeName = "varchar(255)")]
        public string TarjetaProfesional { get; set; }

        [Required]
        [Column(TypeName = "varchar(255)")]
        public string Titulo { get; set; }

        [Column(TypeName = "text")]
        public string Observacion { get; set; }
    }
}
