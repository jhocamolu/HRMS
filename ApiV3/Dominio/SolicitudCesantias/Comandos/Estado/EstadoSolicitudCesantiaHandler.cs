using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.SolicitudCesantias.Comandos.Estado
{
    public class EstadoSolicitudCesantiaHandler : IRequestHandler<EstadoSolicitudCesantiaRequest, CommandResult>
    {
        private NominaDbContext contexto;

        public EstadoSolicitudCesantiaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EstadoSolicitudCesantiaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var solicitudCesantias = contexto.SolicitudCesantias.Find(request.Id);
                if (request.Estado != null)
                {
                    if (request.Estado == EstadoCesantia.Aprobada)
                    {
                        solicitudCesantias.Estado = EstadoCesantia.Aprobada;
                    }
                    if (request.Estado == EstadoCesantia.Cancelada)
                    {
                        solicitudCesantias.Estado = EstadoCesantia.Cancelada;
                    }
                    if (request.Estado == EstadoCesantia.Rechazada)
                    {
                        solicitudCesantias.Estado = EstadoCesantia.Rechazada;
                    }
                }
                if (request.Justificacion != null)
                {
                    solicitudCesantias.Justificacion = request.Justificacion;
                }

                contexto.SolicitudCesantias.Update(solicitudCesantias);
                await contexto.SaveChangesAsync();


                return CommandResult.Success(solicitudCesantias);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
