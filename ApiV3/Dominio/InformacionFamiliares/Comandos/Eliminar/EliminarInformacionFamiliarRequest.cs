using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.InformacionFamiliares.Comandos.Eliminar
{
    public class EliminarInformacionFamiliarRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
