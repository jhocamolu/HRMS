using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.FuncionarioEstudios.Comandos.Eliminar
{
    public class EliminarFuncionarioEstudioHandler : IRequestHandler<EliminarFuncionarioEstudioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarFuncionarioEstudioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarFuncionarioEstudioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                FuncionarioEstudio funcionarioEstudio = await this.contexto.FuncionarioEstudios.FindAsync(request.Id);
                this.contexto.FuncionarioEstudios.Remove(funcionarioEstudio);
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
