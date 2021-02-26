using ApiV3.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApiV3.Dominio.TipoDocumentos.Consultas.ObtenerTipoDocuemnto
{
    public class ObtenerTipoDocumentoRequest : IRequest<TipoDocumento>
    {
        [Required(ErrorMessage = "Este campo es requerido")]
        public int Id { get; set; }
    }
}