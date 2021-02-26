using ApiV3.Infraestructura.Enumerador;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("SolicitudCesantia")]
    public class SolicitudCesantia : AuditoriaRegistro
    {
        [Key]
        [Required]
        [Display(Description = "llave")]
        public int Id { get; set; }

        [Required]
        [Display(Description = "Código que identifica el funcionario.")]
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }

        [Required]
        [Display(Description = "Código que identifica el funcionario.")]
        public int MotivoSolicitudCesantiaId { get; set; }
        public virtual MotivoSolicitudCesantia MotivoSolicitudCesantia { get; set; }

        [Required]
        [Display(Description = "Fecha en la que se realiza la solicitud de cesantías.")]
        public DateTime FechaSolicitud { get; set; }

        [Required]
        [Display(Description = "Base con la que se realiza el cálculo del anticipo de cesantías.")]
        [Column(TypeName = "money")]
        public double BaseCalculoCesantia { get; set; }

        [Required]
        [Display(Description = "Valor que solicita el funcionario para pago de cesantías.")]
        [Column(TypeName = "money")]
        public double ValorSolicitado { get; set; }

        [Display(Description = "Identificador del documento adjunto.")]
        [Column(TypeName = "varchar(255)")]
        public string Soporte { get; set; }

        [Required]
        [Display(Description = "Descripción de la solicitud.")]
        [Column(TypeName = "varchar(255)")]
        public string Observacion { get; set; }

        [Display(Description = "Respuesta de la solicitud.")]
        [Column(TypeName = "text")]
        public string Justificacion { get; set; }

        [Required]
        [Display(Description = "Estado de la solicitud de cesantías.")]
        [Column(TypeName = "varchar(255)")]
        public EstadoCesantia Estado { get; set; }
    }
}
