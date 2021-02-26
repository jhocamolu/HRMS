using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Libranzas.Comandos.Eliminar
{
    public class EliminarLibranzaRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
