using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.AusentismoFuncionarios.Comandos.Estado
{
    public class EstadoAusentismoFuncionarioHandler : IRequestHandler<EstadoAusentismoFuncionarioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EstadoAusentismoFuncionarioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(EstadoAusentismoFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                AusentismoFuncionario ausentismoFuncionario = this.contexto.AusentismoFuncionarios.Find(request.Id);
                ausentismoFuncionario.Estado = request.Estado;
                if (request.Justificacion != null)
                {
                    ausentismoFuncionario.Justificacion = request.Justificacion;
                }
                contexto.AusentismoFuncionarios.Update(ausentismoFuncionario);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(ausentismoFuncionario);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
