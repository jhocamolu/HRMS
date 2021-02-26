using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoPeriodos.Comandos.Actualizar
{
    public class ActualizarTipoPeriodoHandler : IRequestHandler<ActualizarTipoPeriodoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarTipoPeriodoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarTipoPeriodoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoPeriodo tipoPeriodo = this.contexto.TipoPeriodos.Find(request.Id);
                tipoPeriodo.Nombre = Texto.TipoOracion(request.Nombre);
                tipoPeriodo.PagoPorDefecto = (bool)request.PagoPorDefecto;

                this.contexto.TipoPeriodos.Update(tipoPeriodo);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(tipoPeriodo);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
