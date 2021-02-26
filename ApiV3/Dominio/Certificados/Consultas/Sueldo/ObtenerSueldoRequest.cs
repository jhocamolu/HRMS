using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Certificados.Consultas.Sueldo
{
    public class ObtenerSueldoRequest : IRequest<CommandResult>
    {
        public string Id { get; set; }
    }
}
