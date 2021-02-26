using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ApiV3.Dominio.NominaFuncionarios.Comandos.Listar
{
    public class ListarNominaFuncionarioRequest : IRequest<CommandResult>
    {
        [Required(ErrorMessage = ConstantesErrores.Requerido)]
        public int? NominaId { get; set; }

        public string Funcionario { get; set; }

        public int? CentroOperativoId { get; set; }

        public int? DependenciaId { get; set; }

        public int? GrupoNominaId { get; set; }

    }
}
