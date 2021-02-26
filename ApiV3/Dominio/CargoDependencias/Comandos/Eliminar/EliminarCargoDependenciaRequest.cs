using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.CargoDependencias.Comandos.Eliminar
{
    public class EliminarCargoDependenciaRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
