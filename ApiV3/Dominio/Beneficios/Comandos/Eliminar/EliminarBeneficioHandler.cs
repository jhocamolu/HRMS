using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Beneficios.Comandos.Eliminar
{
    public class EliminarBeneficioHandler : IRequestHandler<EliminarBeneficioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarBeneficioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarBeneficioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Beneficio beneficio = await this.contexto.Beneficios.FindAsync(request.Id);
                if (beneficio == null)
                {
                    CommandResult.Fail("No existe", 404);
                }
                this.contexto.Beneficios.Remove(beneficio);
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