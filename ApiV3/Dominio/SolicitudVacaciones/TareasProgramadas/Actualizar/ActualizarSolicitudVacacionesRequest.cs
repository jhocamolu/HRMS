using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.SolicitudVacaciones.TareasProgramadas.Actualizar
{
    public class ActualizarSolicitudVacacionesRequest : IRequest<CommandResult>
    {
        public string Fecha { get; set; }
    }
}
