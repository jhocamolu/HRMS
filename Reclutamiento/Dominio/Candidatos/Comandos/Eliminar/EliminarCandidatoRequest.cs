using MediatR;
using Reclutamiento.Infraestructura.Resultados;
using Reclutamiento.Infraestructura.Utilidades;
using System.ComponentModel.DataAnnotations;

namespace Reclutamiento.Dominio.Candidatos.Comandos.Eliminar
{
    public class EliminarCandidatoRequest : IRequest<CommandResult>
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? Id { get; set; }
    }
}
