using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.Nominas.Comandos.Eliminar
{
    public class EliminarNominaRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}