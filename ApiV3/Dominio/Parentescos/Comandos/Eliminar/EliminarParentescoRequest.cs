using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Parentescos.Comandos.Eliminar
{
    public class EliminarParentescoRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
