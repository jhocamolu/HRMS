using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Beneficios.Consultas
{
    public class ObtenerBeneficioRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}