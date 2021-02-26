using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Nominas.Comandos.Graficas
{
    public class GraficasNominaRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
