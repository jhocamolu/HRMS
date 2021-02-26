using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoHoraExtras.Comandos.Actualizar
{
    public class ActualizarTipoHoraExtraHandler : IRequestHandler<ActualizarTipoHoraExtraRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarTipoHoraExtraHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarTipoHoraExtraRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoHoraExtra tipoHoraExtra = this.contexto.TipoHoraExtras.Find(request.Id);
                tipoHoraExtra.Tipo = request.Tipo;
                tipoHoraExtra.ConceptoNominaId = request.ConceptoNominaId;

                this.contexto.TipoHoraExtras.Update(tipoHoraExtra);
                await this.contexto.SaveChangesAsync();

                return CommandResult.Success(tipoHoraExtra);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
