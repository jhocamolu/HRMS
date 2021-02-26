using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoBeneficioRequisitos.Comandos.Actualizar
{
    public class ActualizarTipoBeneficioRequisitoHandler : IRequestHandler<ActualizarTipoBeneficioRequisitoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarTipoBeneficioRequisitoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarTipoBeneficioRequisitoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoBeneficioRequisito tipoBeneficioRequisito = contexto.TipoBeneficioRequisitos.FirstOrDefault(x => x.Id == request.Id);
                tipoBeneficioRequisito.TipoBeneficioId = request.TipoBeneficioId;
                tipoBeneficioRequisito.TipoSoporteId = request.TipoSoporteId;
                this.contexto.TipoBeneficioRequisitos.Update(tipoBeneficioRequisito);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(tipoBeneficioRequisito);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
