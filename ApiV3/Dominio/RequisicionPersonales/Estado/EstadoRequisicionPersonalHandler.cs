using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.RequisicionPersonales.Estado
{
    public class EstadoRequisicionPersonalHandler : IRequestHandler<EstadoRequisicionPersonalRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EstadoRequisicionPersonalHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EstadoRequisicionPersonalRequest request, CancellationToken cancellationToken)
        {
            try
            {
                RequisicionPersonal requisicion = contexto.RequisicionPersonales.Find(request.Id);
                requisicion.Justificacion = request.Justificacion;
                requisicion.Estado = (EstadoRequisicionPersonal)request.Estado;


                if (request.Estado == EstadoRequisicionPersonal.Anulada)
                {
                    var candidato = contexto.Candidatos.Where(x => x.RequisicionPersonalId == request.Id);

                    foreach (var item in candidato)
                    {
                        item.Estado = EstadoCandidato.Anulado;
                        this.contexto.Update(item);
                    }
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
