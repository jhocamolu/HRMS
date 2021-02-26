using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.NominaFuncionarios.Comandos.Finalizar
{
    public class FinalizarNominaFuncionarioHandler : IRequestHandler<FinalizarNominaFuncionarioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public FinalizarNominaFuncionarioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(FinalizarNominaFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                List<NominaFuncionario> lista = new List<NominaFuncionario>();

                var nomina = contexto.Nominas.FirstOrDefault(x => x.Id == request.NominaId);
                nomina.Estado = EstadoNomina.Liquidada;

                contexto.Nominas.Update(nomina);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(nomina);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
