using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.GrupoNominas.Comandos.Eliminar
{
    public class EliminarGrupoNominaRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
