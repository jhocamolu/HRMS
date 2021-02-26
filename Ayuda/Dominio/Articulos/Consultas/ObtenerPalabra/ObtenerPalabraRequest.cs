using Ayuda.Infraestructura;
using Ayuda.Infraestructura.Resultados;
using MediatR;

namespace Ayuda.Dominio.Articulos.Consultas.ObtenerPalabra
{
    public class ObtenerPalabraRequest : EnumerableRequest, IRequest<QueryResult>
    {
        public string Palabra { get; set; }
    }
}
