using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.ActividadEconomicas.Comandos.Eliminar
{
    public class EliminarActividadEcomonicaRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}