using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.EntidadesFinancieras.Comandos.Eliminar
{
    public class EliminarEntidadFinancieraRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
