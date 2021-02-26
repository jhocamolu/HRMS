using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Terceros.Comandos.Eliminar
{
    public class EliminarTerceroRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
