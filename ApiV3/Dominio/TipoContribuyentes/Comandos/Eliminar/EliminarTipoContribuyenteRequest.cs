using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.TipoContribuyentes.Comandos.Eliminar
{
    public class EliminarTipoContribuyenteRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }

    }
}
