using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.CuentaContables.TareaProgramada
{
    public class ObtenerCuentaContableRequest : IRequest<CommandResult>
    {
        public string Fecha { get; set; }
    }

}
