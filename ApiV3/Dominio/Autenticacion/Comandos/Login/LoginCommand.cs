using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Autenticacion.Comandos.Login
{
    public class LoginCommand : IRequest<CommandResult>
    {

        public string Cedula { get; set; }

        public string Clave { get; set; }

    }
}
