using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.RequisicionPersonales.Parcial
{
    public class ParcialRequisicionPersonalHandler : IRequestHandler<ParcialRequisicionPersonalRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialRequisicionPersonalHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialRequisicionPersonalRequest request, CancellationToken cancellationToken)
        {
            try
            {
                RequisicionPersonal requisicion = await contexto.RequisicionPersonales.FindAsync(request.Id);
                requisicion.EstadoRegistro = EstadoRegistro.Activo;
                if (request.Activo == false)
                {
                    requisicion.EstadoRegistro = EstadoRegistro.Inactivo;
                }

                this.contexto.RequisicionPersonales.Update(requisicion);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(requisicion);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
