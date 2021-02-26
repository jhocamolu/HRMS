using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoAusentismos.Comandos.Actualizar
{
    public class ActualizarTipoAusentismoCommand : IRequestHandler<ActualizarTipoAusentismoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarTipoAusentismoCommand(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarTipoAusentismoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoAusentismo tipoAusentismo = this.contexto.TipoAusentismos.Find(request.Id);

                tipoAusentismo.Nombre = Texto.TipoOracion(request.Nombre);
                tipoAusentismo.ClaseAusentismoId = (int)request.ClaseAusentismoId;
                tipoAusentismo.UnidadTiempo = (UnidadTiempo)request.UnidadTiempo;

                this.contexto.TipoAusentismos.Update(tipoAusentismo);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(tipoAusentismo);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
