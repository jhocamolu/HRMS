using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.RangoUvts.Comandos.Parcial
{
    public class ParcialRangoUvtHandlerActualizarRangoUvtHandler : IRequestHandler<ParcialRangoUvtRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialRangoUvtHandlerActualizarRangoUvtHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialRangoUvtRequest request, CancellationToken cancellationToken)
        {
            try
            {
                RangoUvt rangoUvt = await this.contexto.RangoUvts.FindAsync(request.Id);
                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        rangoUvt.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    if (request.Activo == false)
                    {
                        rangoUvt.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }

                contexto.RangoUvts.Update(rangoUvt);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(rangoUvt);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
