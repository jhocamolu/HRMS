using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoGastoViajes.Comandos.Crear
{
    public class CrearTipoGastoViajeHandler : IRequestHandler<CrearTipoGastoViajeRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearTipoGastoViajeHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearTipoGastoViajeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoGastoViaje tipoGastoViaje = new TipoGastoViaje();
                tipoGastoViaje.Tipo = (TipoGastosViaje)request.Tipo;
                tipoGastoViaje.ConceptoNominaId = (int)request.ConceptoNominaId;
                contexto.TipoGastoViajes.Add(tipoGastoViaje);
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
