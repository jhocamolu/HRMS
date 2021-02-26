using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.EjecucionTareas.Comandos.ActualizarEstadoFuncionarioContrato
{
    public class ActualizarEstadoFuncionarioContratoRequest : IRequest<CommandResult>
    {
        public string Fecha { get; set; }
    }
}
