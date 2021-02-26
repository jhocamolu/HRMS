using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.TipoSoportes.Comandos.Eliminar
{
    public class EliminarTipoSoporteRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
