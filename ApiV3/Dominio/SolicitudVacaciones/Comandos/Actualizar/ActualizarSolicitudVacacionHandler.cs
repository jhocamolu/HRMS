using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.SolicitudVacaciones.Comandos.Actualizar
{
    public class ActualizarSolicitudVacacionHandler : IRequestHandler<ActualizarSolicitudVacacionRequest, CommandResult>
    {
        private NominaDbContext contexto;

        public ActualizarSolicitudVacacionHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarSolicitudVacacionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                SolicitudVacacion solicitudVacacion = contexto.SolicitudVacaciones.Find(request.Id);
                solicitudVacacion.FuncionarioId = (int)request.FuncionarioId;
                solicitudVacacion.LibroVacacionesId = (int)request.LibroVacacionesId;
                solicitudVacacion.FechaInicioDisfrute = (DateTime)request.FechaInicioDisfrute;
                solicitudVacacion.DiasDisfrute = (int)request.DiasDisfrute;
                solicitudVacacion.FechaFinDisfrute = request.FechaFinDisfrute;
                solicitudVacacion.DiasDinero = (int)request.DiasDinero;
                solicitudVacacion.Observacion = request.Observacion;
                solicitudVacacion.FechaRegreso = request.FechaFinDisfrute.AddDays(1);
                solicitudVacacion.FechaPago = DateTime.Parse(request.FechaInicioDisfrute.ToString()).AddDays(-1);

                contexto.SolicitudVacaciones.Update(solicitudVacacion);
                await contexto.SaveChangesAsync();

                LibroVacacion libro = contexto.LibroVacaciones.FirstOrDefault(x => x.Id == (int)request.LibroVacacionesId);
                libro.DiasDisponibles = libro.DiasDisponibles - ((int)request.DiasDinero + (int)request.DiasDisfrute);
                contexto.LibroVacaciones.Update(libro);
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
