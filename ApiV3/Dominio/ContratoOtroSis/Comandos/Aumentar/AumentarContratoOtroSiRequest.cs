using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiV3.Dominio.ContratoOtroSis.Comandos.Aumentar
{
    public class AumentarContratoOtroSiRequest : IRequest<CommandResult>
    {
        #region Validacion

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public DateTime FechaAplicacion { get; set; }


        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [Range(minimum: 1, maximum: 9999999999999, ErrorMessage = ConstantesErrores.Rango + "1 - 9999999999999.")]
        public double? PorcentajeAplicacion { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        [EnumDataType(typeof(AplicacionAumentarSalario), ErrorMessage = "No es una aplicación valida.")]
        public AplicacionAumentarSalario? Aplicar { get; set; }

        #endregion
    }
}
