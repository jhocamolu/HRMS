using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.ExperienciaLaborales.Comandos.Eliminar
{
    public class EliminarExperienciaLaboralRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
