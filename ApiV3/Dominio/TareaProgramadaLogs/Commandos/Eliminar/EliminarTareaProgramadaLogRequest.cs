using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.TareaProgramadaLogs.Commandos.Eliminar
{
    public class EliminarTareaProgramadaLogRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
