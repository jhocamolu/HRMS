using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.HoraExtras.Comandos.Eliminar
{
    public class EliminarHoraExtraRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
