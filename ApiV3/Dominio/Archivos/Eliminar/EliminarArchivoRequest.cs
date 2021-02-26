using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Archivos.Eliminar
{
    public class EliminarArchivoRequest : IRequest<CommandResult>
    {
        public string Id { get; set; }
    }
}
