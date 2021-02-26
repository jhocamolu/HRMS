using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.TipoEmbargoConceptoNominas.Comandos.Eliminar
{
    public class EliminarTipoEmbargoConceptoNominaRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
