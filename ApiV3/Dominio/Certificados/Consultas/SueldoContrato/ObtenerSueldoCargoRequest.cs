using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Certificados.Consultas.SueldoContrato
{
    public class ObtenerSueldoCargoRequest : IRequest<CommandResult>
    {
        public string Id { get; set; }
    }
}
