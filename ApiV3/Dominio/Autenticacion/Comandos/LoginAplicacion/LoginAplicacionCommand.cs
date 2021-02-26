using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Autenticacion.Comandos.LoginAplicacion
{
    public class LoginAplicacionCommand : IRequest<CommandResult>
    {
        public string Id { get; set; }
        public string Secret { get; set; }
    }
}
