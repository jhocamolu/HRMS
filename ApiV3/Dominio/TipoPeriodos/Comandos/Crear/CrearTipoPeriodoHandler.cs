using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoPeriodos.Comandos.Crear
{
    public class CrearTipoPeriodoHandler : IRequestHandler<CrearTipoPeriodoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearTipoPeriodoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearTipoPeriodoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoPeriodo tipoPeriodo = new TipoPeriodo();
                tipoPeriodo.Nombre = Texto.TipoOracion(request.Nombre);
                tipoPeriodo.PagoPorDefecto = (bool)request.PagoPorDefecto;

                this.contexto.TipoPeriodos.Add(tipoPeriodo);
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
