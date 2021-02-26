using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.SolicitudCesantias.Consultas
{
    public class ObtenerDatosCesantiasRequest : IRequest<CommandResult>
    {
        public int FuncionarioId { get; set; }
    }
}
