using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoAusentismos.Comandos.Parcial
{
    public class ParcialTipoAusentismoHandler : IRequestHandler<ParcialTipoAusentismoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialTipoAusentismoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialTipoAusentismoRequest request, CancellationToken cancellationToken)
        {
            TipoAusentismo tipoAusentismo = this.contexto.TipoAusentismos.Find(request.Id);

            if (request.Activo != null)
            {
                if (request.Activo == true)
                {
                    tipoAusentismo.EstadoRegistro = EstadoRegistro.Activo;
                }
                else
                {
                    tipoAusentismo.EstadoRegistro = EstadoRegistro.Inactivo;
                }
            }

            if (request.Nombre != null)
            {
                tipoAusentismo.Nombre = Texto.TipoOracion(request.Nombre);
            }
            if (request.ClaseAusentismoId != null)
            {
                tipoAusentismo.ClaseAusentismoId = (int)request.ClaseAusentismoId;
            }
            if (request.UnidadTiempo != null)
            {
                tipoAusentismo.UnidadTiempo = (UnidadTiempo)request.UnidadTiempo;
            }

            this.contexto.TipoAusentismos.Update(tipoAusentismo);
            await this.contexto.SaveChangesAsync();
            return CommandResult.Success(tipoAusentismo);
        }
    }
}
