using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoBeneficios.Comandos.Parcial
{
    public class ParcialTipoBeneficioHandler : IRequestHandler<ParcialTipoBeneficioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialTipoBeneficioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialTipoBeneficioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoBeneficio tipoBeneficio = contexto.TipoBeneficios.FirstOrDefault(x => x.Id == request.Id);

                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        tipoBeneficio.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        tipoBeneficio.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }

                this.contexto.TipoBeneficios.Update(tipoBeneficio);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(tipoBeneficio);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
