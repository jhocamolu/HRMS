using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.NominaFuncionarios.Comandos.Eliminar
{
    public class EliminarNominaFuncionarioHandler : IRequestHandler<EliminarNominaFuncionarioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarNominaFuncionarioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarNominaFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                int nominaId = 0;
                int eliminar;
                if (request.Id != null && request.Funcionarios == null && request.NominaId == null)
                {
                    nominaId = contexto.NominaFuncionarios.FirstOrDefault(x => x.Id == request.Id).NominaId;
                    eliminar = await contexto.Database.ExecuteSqlRawAsync($"DELETE FROM NominaFuncionario WHERE Id={request.Id}");
                }
                else if (request.Funcionarios != null && request.NominaId != null)
                {
                    foreach (var item in request.Funcionarios)
                    {
                        eliminar = await contexto.Database.ExecuteSqlRawAsync($"DELETE FROM NominaFuncionario WHERE NominaId={request.NominaId} AND FuncionarioId={item}");
                        nominaId = (int)request.NominaId;
                    }
                }
                else if (request.Funcionarios == null && request.Id == null && request.NominaId != null)
                {
                    eliminar = await contexto.Database.ExecuteSqlRawAsync($"DELETE FROM NominaFuncionario WHERE NominaId={request.NominaId}");
                    nominaId = (int)request.NominaId;
                }

                var nominaFuncionario = contexto.NominaFuncionarios.Where(x => x.NominaId == nominaId).ToList();
                var nomina = contexto.Nominas.FirstOrDefault(x => x.Id == request.NominaId);
                if (!nominaFuncionario.Any())
                {
                    nomina.Estado = EstadoNomina.Inicializada;
                    contexto.Nominas.Update(nomina);
                    await contexto.SaveChangesAsync();
                }

                return CommandResult.Success(nominaFuncionario);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
