using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApiV3.Dominio.Reportes.Comandos.DesprendiblePagos
{
    public class ReporteDesprendiblePagoRequest : IRequest<CommandResult>
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public string NominaFuncionarioId { get; set; }
    }
}
