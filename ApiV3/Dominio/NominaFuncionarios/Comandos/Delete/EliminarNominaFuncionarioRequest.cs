using ApiV3.Infraestructura.Resultados;
using MediatR;
using System.Collections.Generic;

namespace ApiV3.Dominio.NominaFuncionarios.Comandos.Eliminar
{
    public class EliminarNominaFuncionarioRequest : IRequest<CommandResult>
    {
        public int? Id { get; set; }

        public int? NominaId { get; set; }

        public List<int> Funcionarios { get; set; }
    }
}
