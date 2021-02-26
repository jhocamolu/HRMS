using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.SubPeriodos.Comandos.Actualizar
{
    public class ActualizarSubPeriodoHandler : IRequestHandler<ActualizarSubPeriodoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarSubPeriodoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarSubPeriodoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                SubPeriodo subPeriodo = this.contexto.SubPeriodos.Find(request.Id);
                subPeriodo.TipoPeriodoId = (int)request.TipoPeriodoId;
                subPeriodo.Nombre = Texto.TipoOracion(request.Nombre);
                subPeriodo.Dias = (int)request.Dias;
                subPeriodo.DiaInicial = (int)request.DiaInicial;

                this.contexto.SubPeriodos.Update(subPeriodo);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(subPeriodo);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
