using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;

namespace ApiV3.Dominio.LibroVacaciones.TareasProgramadas.Migrar
{
    public class MigrarLibroVacacionesRequest : IRequest<CommandResult>
    {
        public DateTime? Fecha { get; set; }
    }
}
