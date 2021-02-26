using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Autenticacion.Comandos.PermisoAplicacion
{
    public class PermisoAplicacionCommand : IRequest<CommandResult>
    {

        public string token { get; set; }

        public string aplicacion { get; set; }

    }
}
