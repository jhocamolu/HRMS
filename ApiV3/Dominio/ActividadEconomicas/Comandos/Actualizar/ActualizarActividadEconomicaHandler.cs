using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ActividadEconomicas.Comandos.Actualizar
{
    public class ActualizarActividadEconomicaHandler : IRequestHandler<ActualizarActividadEconomicaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarActividadEconomicaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarActividadEconomicaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ActividadEconomica actividadEconomica = this.contexto.ActividadEconomicas.Find(request.Id);
                actividadEconomica.Nombre = request.Nombre;
                actividadEconomica.Codigo = request.Codigo;
                this.contexto.ActividadEconomicas.Update(actividadEconomica);
                await this.contexto.SaveChangesAsync();

                return CommandResult.Success(actividadEconomica);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
