
using MediatR;
using Reclutamiento.Infraestructura.Resultados;
using Reclutamiento.Infraestructura.Utilidades;
using System.ComponentModel.DataAnnotations;

namespace Reclutamiento.Dominio.HojaDeVidaDocumentos.Comandos.Eliminar
{
    public class EliminarHojaDeVidaDocumentoRequest : IRequest<CommandResult>
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; set; }
    }
}
