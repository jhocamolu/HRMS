using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApiV3.Dominio.ConceptoNominas.Comandos.Eliminar
{
    /// <summary>
    /// Clase encargada de realizar validaciones para eliminar  ConceptosNomina
    /// </summary>
    public class EliminarConceptoNominaRequest : IRequest<CommandResult>
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Id { get; set; }
    }
}
