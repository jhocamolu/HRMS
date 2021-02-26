using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Certificados.Consultas.Contrato
{
    public class ObtenerCargoRequest : IRequest<CommandResult>
    {
        public string Id { get; set; }
    }
}
