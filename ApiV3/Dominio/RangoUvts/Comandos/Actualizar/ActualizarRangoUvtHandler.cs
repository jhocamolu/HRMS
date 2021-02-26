using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.RangoUvts.Comandos.Actualizar
{
    public class ActualizarRangoUvtHandler : IRequestHandler<ActualizarRangoUvtRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarRangoUvtHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarRangoUvtRequest request, CancellationToken cancellationToken)
        {
            try
            {
                RangoUvt rangoUvt = await this.contexto.RangoUvts.FindAsync(request.Id);
                rangoUvt.Desde = (int)request.Desde;
                rangoUvt.Hasta = request.Hasta;
                rangoUvt.Porcentaje = (decimal)request.Porcentaje;
                rangoUvt.Adiciona = (int)request.Adiciona;
                rangoUvt.Sustrae = (int)request.Sustrae;
                rangoUvt.ValidoDesde = (DateTime)request.ValidoDesde;

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
