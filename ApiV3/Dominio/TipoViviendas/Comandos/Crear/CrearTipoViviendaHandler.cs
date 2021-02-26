using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoViviendas.Comandos.Crear
{
    public class CrearTipoViviendaHandler : IRequestHandler<CrearTipoViviendaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearTipoViviendaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearTipoViviendaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoVivienda tipoVivienda = new TipoVivienda
                {
                    Nombre = Texto.TipoOracion(request.Nombre.ToLower())
                };
                this.contexto.TipoViviendas.Add(tipoVivienda);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(tipoVivienda);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
