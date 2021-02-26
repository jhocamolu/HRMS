using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Libranzas.TareaProgramada.IniciarVigencia
{
    public class IniciarVigenciaLibranzaRequest : IRequest<CommandResult>
    {
        public string Fecha { get; set; }
    }
}
