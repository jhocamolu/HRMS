using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.SolicitudVacaciones.Comandos.Parcial
{
    public class ParcialSolicitudVacacionHandler : IRequestHandler<ParcialSolicitudVacacionRequest, CommandResult>
    {
        private NominaDbContext contexto;

        public ParcialSolicitudVacacionHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialSolicitudVacacionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                SolicitudVacacion solicitudVacacion = contexto.SolicitudVacaciones.Find(request.Id);
                if (request.FuncionarioId != null)
                {
                    solicitudVacacion.FuncionarioId = (int)request.FuncionarioId;
                }
                if (request.LibroVacacionesId != null)
                {
                    solicitudVacacion.LibroVacacionesId = (int)request.LibroVacacionesId;
                }
                if (request.FechaInicioDisfrute != null)
                {
                    solicitudVacacion.FechaInicioDisfrute = (DateTime)request.FechaInicioDisfrute;
                    solicitudVacacion.FechaPago = DateTime.Parse(request.FechaInicioDisfrute.ToString()).AddDays(-1);
                }
                if (request.DiasDisfrute != null)
                {
                    solicitudVacacion.DiasDisfrute = (int)request.DiasDisfrute;
                }
                if (request.FechaFinDisfrute != null)
                {
                    solicitudVacacion.FechaFinDisfrute = request.FechaFinDisfrute;
                    solicitudVacacion.FechaRegreso = request.FechaFinDisfrute.AddDays(1);
                }
                if (request.DiasDinero != null)
                {
                    solicitudVacacion.DiasDinero = (int)request.DiasDinero;
                }
                if (request.Observacion != null)
                {
                    solicitudVacacion.Observacion = request.Observacion;
                }

                contexto.SolicitudVacaciones.Update(solicitudVacacion);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(solicitudVacacion);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
