using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.AplicacionExternaCargos.Comandos.Eliminar
{
    public class EliminarAplicacionExternaCargoRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
