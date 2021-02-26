using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.BeneficioAdjuntos.Comandos.Eliminar
{
    public class EliminarBeneficioAdjuntoHandler : IRequestHandler<EliminarBeneficioAdjuntoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarBeneficioAdjuntoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarBeneficioAdjuntoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                BeneficioAdjunto beneficioAdjunto = await contexto.BeneficioAdjuntos.FindAsync(request.Id);
                if (beneficioAdjunto == null)
                {
                    CommandResult.Fail("No existe", 404);
                }
                contexto.BeneficioAdjuntos.Remove(beneficioAdjunto);
                await contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
