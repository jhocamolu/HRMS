using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoBeneficioRequisitos.Comandos.Eliminar
{
    public class EliminarTipoBeneficioRequisitoHandler : IRequestHandler<EliminarTipoBeneficioRequisitoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarTipoBeneficioRequisitoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarTipoBeneficioRequisitoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoBeneficioRequisito tipoBeneficioRequisito = contexto.TipoBeneficioRequisitos.FirstOrDefault(x => x.Id == request.Id);

                this.contexto.TipoBeneficioRequisitos.Remove(tipoBeneficioRequisito);
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
