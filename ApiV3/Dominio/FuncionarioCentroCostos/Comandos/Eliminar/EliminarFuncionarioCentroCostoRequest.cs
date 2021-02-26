using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApiV3.Dominio.FuncionarioCentroCostos.Comandos.Eliminar
{
    public class EliminarFuncionarioCentroCostoRequest : IRequest<CommandResult>
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int FuncionarioId { get; set; }
    }
}
