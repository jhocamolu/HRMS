using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.TipoEmbargos.Comandos.Eliminar
{
    public class EliminarTipoEmbargoRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
