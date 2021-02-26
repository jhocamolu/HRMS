using ApiV3.Infraestructura.Enumerador;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiV3.Models
{
    [Table("Beneficio")]
    public class Beneficio : AuditoriaRegistro
    {
        [Key]
        [Required]
        [Display(Description = "llave")]
        public int Id { get; set; }


        [Required]
        [Display(Description = "Documento de identificación del funcionario.")]
        public int FuncionarioId { get; set; }
        public virtual Funcionario Funcionario { get; set; }


        [Required]
        [Display(Description = "Beneficio que al que puede acceder el  funcionario.")]
        public int TipoBeneficioId { get; set; }
        public virtual TipoBeneficio TipoBeneficio { get; set; }


        [Required]
        [Column(TypeName = "date")]
        [Display(Description = "Fecha en la que se realiza el trámite de la solicitud.")]
        public DateTime FechaSolicitud { get; set; }



        [Column(TypeName = "money")]
        [Display(Description = "Monto de dinero solicitado.")]
        public double? ValorSolicitud { get; set; }


        [Display(Description = "Cantidad de cuotas a pagar.")]
        public int? PlazoMaximo { get; set; }


        [Display(Description = "Indica en qué período de pago.")]
        public int? TipoPeriodoId { get; set; }
        public virtual TipoPeriodo TipoPeriodo { get; set; }


        [Column(TypeName = "varchar(100)")]
        [Display(Description = "Opción de auxilio educativo.")]
        public OpcionAuxilioEducativo? OpcionAuxilioEducativo { get; set; }


        [Display(Description = "Cantidad de horas por semana que debe estudiar el funcionario.")]
        public int? CantidadHoraSemana { get; set; }


        [Column(TypeName = "date")]
        [Display(Description = "Fecha en la que inicia estudio el funcionario.")]
        public DateTime? FechaInicioEstudio { get; set; }

        [Column(TypeName = "float")]
        [Display(Description = "Nota académica del funcionario.")]
        public float? NotaAcademica { get; set; }


        [Column(TypeName = "date")]
        [Display(Description = "Fecha en la que finaliza estudios el funcionario.")]
        public DateTime? FechaFinalizacionEstudio { get; set; }


        [Column(TypeName = "text")]
        [Display(Description = "Ingrese una descripción.")]
        public string Observacion { get; set; }


        [Column(TypeName = "varchar(100)")]
        [Display(Description = "Seleccione el estado de la solicitud.")]
        public EstadoBeneficiosCorportativos Estado { get; set; }


        [Column(TypeName = "text")]
        [Display(Description = "")]
        public string ObservacionAprobacion { get; set; }


        [Column(TypeName = "text")]
        [Display(Description = "")]
        public string ObservacionAutorizacion { get; set; }


        [Column(TypeName = "text")]
        [Display(Description = "Ingrese una descripción de la nota academica.")]
        public string ObservacionNotaAcademica { get; set; }

        [Column(TypeName = "money")]
        [Display(Description = "Monto de dinero autorizado.")]
        public double? ValorAutorizado { get; set; }


        [Column(TypeName = "money")]
        [Display(Description = "")]
        public double? ValorCobrar { get; set; }

        [Required]
        [Column(TypeName = "money")]
        [Display(Description = "")]
        public double? Saldo { get; set; }

    }
}
