
using MediatR;
using Reclutamiento.Infraestructura.Resultados;

namespace Reclutamiento.Dominio.HojaDeVidaExperienciaLaborales.Comandos.Eliminar
{
    public class EliminarHojaDeVidaExperienciaLaboralRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
