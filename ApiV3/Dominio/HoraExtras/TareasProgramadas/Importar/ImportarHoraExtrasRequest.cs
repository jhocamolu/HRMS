using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiV3.Dominio.HoraExtras.TareasProgramadas.Importar
{
    public class ImportarHoraExtrasRequest:IRequest<CommandResult>
    {
        public DateTime? FechaInicial { get; set; }
        public DateTime? FechaFinal { get; set; }
    }
}
