using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.CargoReportas.Eliminar
{
    public class EliminarCargoReportaRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
