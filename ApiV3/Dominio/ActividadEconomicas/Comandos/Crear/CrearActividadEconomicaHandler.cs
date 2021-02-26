using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ActividadEconomicas.Comandos.Crear
{
    public class CrearActividadEconomicaHandler : IRequestHandler<CrearActividadEconomicaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearActividadEconomicaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearActividadEconomicaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ActividadEconomica actividadEconomicas = new ActividadEconomica
                {
                    Nombre = request.Nombre,
                    Codigo = request.Codigo
                };
                this.contexto.ActividadEconomicas.Add(actividadEconomicas);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(actividadEconomicas);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
