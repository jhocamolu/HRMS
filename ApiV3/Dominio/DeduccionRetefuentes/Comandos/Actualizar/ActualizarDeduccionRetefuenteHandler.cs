using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.DeduccionRetefuentes.Comandos.Actualizar
{
    public class ActualizarDeduccionRetefuenteHandler : IRequestHandler<ActualizarDeduccionRetefuenteRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ActualizarDeduccionRetefuenteHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarDeduccionRetefuenteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                DeduccionRetefuente deduccionRetefuente = await this.contexto.DeduccionRetefuentes.FindAsync(request.Id);
                deduccionRetefuente.InteresVivienda = request.InteresVivienda;
                deduccionRetefuente.MedicinaPrepagada = request.MedicinaPrepagada;

                contexto.DeduccionRetefuentes.Update(deduccionRetefuente);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(deduccionRetefuente);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
