using MediatR;
using System;

namespace ApiV3.Dominio.Autenticacion.Comandos.Loguot
{
    public class LogoutCommand : IRequest<Boolean>
    {

        public string JwtToken { get; set; }

    }
}
