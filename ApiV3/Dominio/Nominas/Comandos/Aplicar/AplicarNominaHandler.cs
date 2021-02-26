using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Nominas.Comandos.Aplicar
{
    public class AplicarNominaHandler : IRequestHandler<AplicarNominaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public AplicarNominaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(AplicarNominaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var nomina = contexto.Nominas.FirstOrDefault(x => x.Id == request.Id);
                nomina.Estado = EstadoNomina.Aplicada;

                contexto.Nominas.Update(nomina);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(nomina);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
