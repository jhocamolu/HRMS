using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApiV3.Dominio.TipoViviendas.Consultas.ObtenerTipoVivienda
{
    public class ObtenerTipoViviendaRequest : IRequest<TipoVivienda>
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int Id { get; internal set; }
    }
}
