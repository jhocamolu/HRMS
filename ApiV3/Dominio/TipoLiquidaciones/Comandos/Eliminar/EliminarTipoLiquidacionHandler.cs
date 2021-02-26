using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoLiquidaciones.Comandos.Eliminar
{
    public class EliminarTipoLiquidacionHandler : IRequestHandler<EliminarTipoLiquidacionRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarTipoLiquidacionHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarTipoLiquidacionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoLiquidacion tipoLiquidacion = this.contexto.TipoLiquidaciones.Find(request.Id);
                this.contexto.TipoLiquidaciones.Remove(tipoLiquidacion);
                await this.contexto.SaveChangesAsync();

                List<TipoLiquidacionModulo> modulos = contexto.TipoLiquidacionModulos.Where(x => x.TipoLiquidacionId == request.Id).ToList();
                foreach (var item in modulos)
                {

                    this.contexto.TipoLiquidacionModulos.Remove(item);
                    await contexto.SaveChangesAsync();
                }

                return CommandResult.Success(tipoLiquidacion);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
