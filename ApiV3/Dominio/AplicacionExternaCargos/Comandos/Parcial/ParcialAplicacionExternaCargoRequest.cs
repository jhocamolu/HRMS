using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApiV3.Dominio.AplicacionExternaCargos.Comandos.Parcial
{
    public class ParcialAplicacionExternaCargoRequest : IRequest<CommandResult>
    {
        #region Validaciones
        public int Id { get; set; }

        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public bool? Activo { get; set; }
        #endregion

    }
}
