using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.TipoHoraExtras.Comandos.Eliminar
{
    public class EliminarTipoHoraExtraRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
