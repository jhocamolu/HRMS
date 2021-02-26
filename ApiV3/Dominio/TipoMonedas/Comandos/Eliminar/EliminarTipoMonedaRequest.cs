using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.TipoMonedas.Comandos.Eliminar
{
    public class EliminarTipoMonedaRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
