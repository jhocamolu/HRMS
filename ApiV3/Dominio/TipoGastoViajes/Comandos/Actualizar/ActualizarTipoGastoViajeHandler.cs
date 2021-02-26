using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoGastoViajes.Comandos.Actualizar
{
    public class ActualizarTipoGastoViajeHandler : IRequestHandler<ActualizarTipoGastoViajeRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarTipoGastoViajeHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarTipoGastoViajeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoGastoViaje tipoGastoViaje = this.contexto.TipoGastoViajes.Find(request.Id);
                tipoGastoViaje.Tipo = (TipoGastosViaje)request.Tipo;
                tipoGastoViaje.ConceptoNominaId = (int)request.ConceptoNominaId;

                contexto.TipoGastoViajes.Update(tipoGastoViaje);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(tipoGastoViaje);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
