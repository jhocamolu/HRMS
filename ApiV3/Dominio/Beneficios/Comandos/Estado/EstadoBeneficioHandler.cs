using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Beneficios.Comandos.Estado
{
    public class EstadoBeneficioHandler : IRequestHandler<EstadoBeneficioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EstadoBeneficioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EstadoBeneficioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Beneficio beneficio = await this.contexto.Beneficios.FindAsync(request.Id);
                beneficio.Estado = EstadoBeneficiosCorportativos.Cancelada;

                this.contexto.Beneficios.Update(beneficio);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(beneficio);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
