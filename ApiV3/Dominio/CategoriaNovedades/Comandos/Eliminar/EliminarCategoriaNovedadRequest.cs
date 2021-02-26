using ApiV3.Infraestructura.Resultados;
using MediatR;

namespace ApiV3.Dominio.CategoriaNovedades.Comandos.Eliminar
{
    public class EliminarCategoriaNovedadRequest : IRequest<CommandResult>
    {
        public int Id { get; set; }
    }
}
