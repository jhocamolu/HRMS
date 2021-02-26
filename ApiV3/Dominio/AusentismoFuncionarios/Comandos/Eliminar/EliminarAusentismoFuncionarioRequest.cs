using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.AusentismoFuncionarios.Comandos.Eliminar
{
    public class EliminarAusentismoFuncionarioRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
