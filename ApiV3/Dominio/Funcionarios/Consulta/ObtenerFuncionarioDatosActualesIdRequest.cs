using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Funcionarios.Consulta
{
    public class ObtenerFuncionarioDatosActualesIdRequest : IRequest<CommandResult>
    {
        public int FuncionarioId { get; set; }
    }
}
