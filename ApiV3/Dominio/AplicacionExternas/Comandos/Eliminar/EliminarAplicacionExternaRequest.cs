using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.AplicacionExternas.Comandos.Eliminar
{
    public class EliminarAplicacionExternaRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
