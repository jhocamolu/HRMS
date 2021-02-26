using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.TipoViviendas.Comandos.Eliminar
{
    public class EliminarTipoViviendasRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
