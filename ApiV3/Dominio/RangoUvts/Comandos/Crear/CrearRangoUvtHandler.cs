using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.RangoUvts.Comandos.Crear
{
    public class CrearRangoUvtHandler : IRequestHandler<CrearRangoUvtRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearRangoUvtHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearRangoUvtRequest request, CancellationToken cancellationToken)
        {
            try
            {
                RangoUvt rangoUvt = new RangoUvt();
                rangoUvt.Desde = (int)request.Desde;
                rangoUvt.Hasta = request.Hasta;
                rangoUvt.Porcentaje = (decimal)request.Porcentaje;
                rangoUvt.Adiciona = (int)request.Adiciona;
                rangoUvt.Sustrae = (int)request.Sustrae;
                rangoUvt.ValidoDesde = (DateTime)request.ValidoDesde;

                contexto.RangoUvts.Add(rangoUvt);
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
