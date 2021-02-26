using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoBeneficios.Comandos.Eliminar
{
    public class EliminarTipoBeneficioHandler : IRequestHandler<EliminarTipoBeneficioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarTipoBeneficioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarTipoBeneficioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoBeneficio tipoBeneficio = contexto.TipoBeneficios.FirstOrDefault(x => x.Id == request.Id);

                this.contexto.TipoBeneficios.Remove(tipoBeneficio);
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
