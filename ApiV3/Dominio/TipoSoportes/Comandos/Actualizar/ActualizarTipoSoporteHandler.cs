using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoSoportes.Comandos.Actualizar
{
    public class ActualizarTipoSoporteHandler : IRequestHandler<ActualizarTipoSoporteRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarTipoSoporteHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarTipoSoporteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoSoporte tipoSoporte = this.contexto.TipoSoportes.Find(request.Id);
                tipoSoporte.Nombre = Texto.TipoOracion(request.Nombre.ToLower());

                this.contexto.TipoSoportes.Update(tipoSoporte);
                await this.contexto.SaveChangesAsync();

                return CommandResult.Success(tipoSoporte);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
