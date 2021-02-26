using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Autenticacion.Comandos.RefreshToken
{
    public class RefreshTokenRequest : IRequest<CommandResult>
    {
        public string refreshToken { get; set; }
    }
}
