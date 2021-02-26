using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ActividadEconomicas.Comandos.Parcial
{
    public class ParcialActividadEconomicaHandler : IRequestHandler<ParcialActividadEcomonicaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialActividadEconomicaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialActividadEcomonicaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ActividadEconomica actividadEconomica = this.contexto.ActividadEconomicas.Find(request.Id);
                if (request.Nombre != null)
                {
                    actividadEconomica.Nombre = request.Nombre;
                }
                if (request.Codigo != null)
                {
                    actividadEconomica.Codigo = request.Codigo;
                }
                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        actividadEconomica.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        actividadEconomica.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }

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
