using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;

namespace ApiV3.Dominio.LibroVacaciones.TareasProgramadas.Actualizar
{
    public class ActualizarLibroVacacionesRequest : IRequest<CommandResult>
    {
        public DateTime? Fecha { get; set; }
    }
}
