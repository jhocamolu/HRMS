using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.TipoContratos.Comandos.Eliminar
{
    public class EliminarTipoContratoRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
