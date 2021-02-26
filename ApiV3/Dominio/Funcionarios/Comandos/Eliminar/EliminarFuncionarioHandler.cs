using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de eliminar registros de la entidad Funcionarios.
/// </summary>

namespace ApiV3.Dominio.Funcionarios.Comandos.Eliminar
{
    public class EliminarFuncionarioHandler : IRequestHandler<EliminarFuncionarioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public EliminarFuncionarioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(EliminarFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Funcionario funcionarios = await this.contexto.Funcionarios.FindAsync(request.Id);
                if (funcionarios == null) return CommandResult.Fail("No existe", 404);

                this.contexto.Funcionarios.Remove(funcionarios);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
