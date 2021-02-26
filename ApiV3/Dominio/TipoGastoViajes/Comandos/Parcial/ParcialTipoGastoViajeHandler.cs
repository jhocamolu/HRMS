using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoGastoViajes.Comandos.Parcial
{
    public class ParcialTipoGastoViajeHandler : IRequestHandler<ParcialTipoGastoViajeRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialTipoGastoViajeHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialTipoGastoViajeRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoGastoViaje tipoGastoViaje = this.contexto.TipoGastoViajes.Find(request.Id);
                if (request.Tipo != null)
                {
                    tipoGastoViaje.Tipo = (TipoGastosViaje)request.Tipo;
                }
                if (request.ConceptoNominaId != null)
                {
                    tipoGastoViaje.ConceptoNominaId = (int)request.ConceptoNominaId;
                }
                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        tipoGastoViaje.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        tipoGastoViaje.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
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
