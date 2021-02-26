using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.TareaProgramadas.Comandos.Eliminar
{
    public class EliminarTareaProgramadaRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
