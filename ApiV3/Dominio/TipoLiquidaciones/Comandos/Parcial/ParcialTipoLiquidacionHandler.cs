using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoLiquidaciones.Comandos.Parcial
{
    public class ParcialTipoLiquidacionHandler : IRequestHandler<ParcialTipoLiquidacionRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialTipoLiquidacionHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialTipoLiquidacionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoLiquidacion tipoLiquidacion = this.contexto.TipoLiquidaciones.Find(request.Id);
                if (request.Activo != null)
                {
                    if ((bool)request.Activo)
                    {
                        tipoLiquidacion.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        tipoLiquidacion.EstadoRegistro = EstadoRegistro.Inactivo;
                    }


                    this.contexto.TipoLiquidaciones.Update(tipoLiquidacion);
                    await this.contexto.SaveChangesAsync();

                    List<TipoLiquidacionModulo> modulos = contexto.TipoLiquidacionModulos.Where(x => x.TipoLiquidacionId == request.Id).ToList();
                    foreach (var item in modulos)
                    {
                        if ((bool)request.Activo)
                        {
                            item.EstadoRegistro = EstadoRegistro.Activo;
                        }
                        else
                        {
                            item.EstadoRegistro = EstadoRegistro.Inactivo;
                        }

                        this.contexto.TipoLiquidacionModulos.Update(item);
                        await contexto.SaveChangesAsync();
                    }
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
