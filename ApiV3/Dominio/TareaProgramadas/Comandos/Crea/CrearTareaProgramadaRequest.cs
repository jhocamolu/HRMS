using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApiV3.Dominio.TareaProgramadas.Comandos.Crea
{
    public class CrearTareaProgramadaRequest : IRequest<CommandResult>
    {
        #region Validaciones
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [RegularExpression(@"^[" + ConstantesExpresionesRegulares.Alfabetico +
                                   ConstantesExpresionesRegulares.Numerico +
                                   ConstantesExpresionesRegulares.Espacio + "]*$",
                          ErrorMessage = ConstantesErrores.Alfanumerico)]
        [MinLength(1, ErrorMessage = ConstantesErrores.Minimo + "1.")]
        [MaxLength(100, ErrorMessage = ConstantesErrores.Maximo + "100.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public string Periodicidad { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public string Instruccion { get; set; }

        public bool EnEjecucion { get; set; }
        #endregion
    }
}
