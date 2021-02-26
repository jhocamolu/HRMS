using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoBeneficioRequisitos.Comandos.Parcial
{
    public class ParcialTipoBeneficioRequisitoHandler : IRequestHandler<ParcialTipoBeneficioRequisitoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialTipoBeneficioRequisitoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialTipoBeneficioRequisitoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoBeneficioRequisito tipoBeneficioRequisito = contexto.TipoBeneficioRequisitos.FirstOrDefault(x => x.Id == request.Id);

                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        tipoBeneficioRequisito.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        tipoBeneficioRequisito.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }

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
