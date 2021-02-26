using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;

namespace ApiV3.Dominio.Beneficios.TareasProgramadas
{
    public class ProcesarBeneficioRequest : IRequest<CommandResult>
    {
        public DateTime? Fecha { get; set; }
    }
}
