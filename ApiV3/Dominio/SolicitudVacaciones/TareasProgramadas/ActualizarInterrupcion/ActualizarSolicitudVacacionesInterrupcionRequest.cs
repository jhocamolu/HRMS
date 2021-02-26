using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.SolicitudVacaciones.TareasProgramadas.ActualizarInterrupcion
{
    public class ActualizarSolicitudVacacionesInterrupcionRequest : IRequest<CommandResult>
    {
        public string Fecha { get; set; }

    }
}
