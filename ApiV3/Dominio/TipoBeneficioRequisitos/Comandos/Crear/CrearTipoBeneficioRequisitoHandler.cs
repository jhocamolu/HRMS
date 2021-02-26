using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoBeneficioRequisitos.Comandos.Crear
{
    public class CrearTipoBeneficioRequisitoHandler : IRequestHandler<CrearTipoBeneficioRequisitoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearTipoBeneficioRequisitoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearTipoBeneficioRequisitoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoBeneficioRequisito tipoBeneficioRequisito = new TipoBeneficioRequisito
                {
                    TipoBeneficioId = request.TipoBeneficioId,
                    TipoSoporteId = request.TipoSoporteId
                };
                this.contexto.TipoBeneficioRequisitos.Add(tipoBeneficioRequisito);
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
