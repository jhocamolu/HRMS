using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Archivos.Consultas
{
    public class ObtenerArchivoRequest : IRequest<CommandResult>
    {
        public string Id { get; set; }
    }
}
