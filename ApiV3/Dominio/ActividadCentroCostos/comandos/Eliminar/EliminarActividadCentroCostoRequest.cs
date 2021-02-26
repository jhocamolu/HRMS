using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.ActividadCentroCostos.comandos.Eliminar
{
    public class EliminarActividadCentroCostoRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
