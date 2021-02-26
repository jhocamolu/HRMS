using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.TipoGastoViajes.Comandos.Eliminar
{
    public class EliminarTipoGastoViajeRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
