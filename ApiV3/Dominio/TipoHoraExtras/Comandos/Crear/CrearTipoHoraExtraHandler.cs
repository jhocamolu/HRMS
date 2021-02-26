using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoHoraExtras.Comandos.Crear
{
    public class CrearTipoHoraExtraHandler : IRequestHandler<CrearTipoHoraExtraRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearTipoHoraExtraHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearTipoHoraExtraRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoHoraExtra tipoHoraExtra = new TipoHoraExtra
                {
                    Tipo = request.Tipo,
                    ConceptoNominaId = request.ConceptoNominaId
                };
                this.contexto.TipoHoraExtras.Add(tipoHoraExtra);
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
