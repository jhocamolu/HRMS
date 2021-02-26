using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoViviendas.Comandos.Actualizar
{
    public class ActualizarTipoViviendaHandler : IRequestHandler<ActualizarTipoViviendaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarTipoViviendaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarTipoViviendaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoVivienda tipoVivienda = this.contexto.TipoViviendas.Find(request.Id);
                tipoVivienda.Nombre = Texto.TipoOracion(request.Nombre.ToLower());

                this.contexto.TipoViviendas.Update(tipoVivienda);
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
