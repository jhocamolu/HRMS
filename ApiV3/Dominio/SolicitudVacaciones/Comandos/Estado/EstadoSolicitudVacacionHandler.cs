using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.SolicitudVacaciones.Comandos.Estado
{
    public class EstadoSolicitudVacacionHandler : IRequestHandler<EstadoSolicitudVacacionRequest, CommandResult>
    {
        private NominaDbContext contexto;

        public EstadoSolicitudVacacionHandler(NominaDbContext contexto)
        {
            this.contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }

        public async Task<CommandResult> Handle(EstadoSolicitudVacacionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var solicitudVacaciones = contexto.SolicitudVacaciones.Find(request.Id);
                if (request.Estado != null)
                {
                    solicitudVacaciones.Estado = (EstadoSolicitudVacaciones)request.Estado;

                    if (request.Estado == EstadoSolicitudVacaciones.Anulada ||
                        request.Estado == EstadoSolicitudVacaciones.Cancelada ||
                        request.Estado == EstadoSolicitudVacaciones.Rechazada)
                    {


                        var libroVacacion = contexto.LibroVacaciones.Find(solicitudVacaciones.LibroVacacionesId);
                        libroVacacion.DiasDisponibles = libroVacacion.DiasDisponibles + solicitudVacaciones.DiasDinero + solicitudVacaciones.DiasDisfrute;
                        contexto.LibroVacaciones.Update(libroVacacion);
                        await contexto.SaveChangesAsync();

                    }

                    if (request.Estado == EstadoSolicitudVacaciones.Terminada)
                    {
                        var dias = 0;
                        if ((DateTime)request.FechaFinDisfrute.Value.Date < solicitudVacaciones.FechaFinDisfrute.Date)
                        {
                            dias = DiaHabil.CantidadDiasHabilesEntreFechas((DateTime)request.FechaFinDisfrute, solicitudVacaciones.FechaFinDisfrute, contexto);
                        }

                        solicitudVacaciones.FechaFinDisfrute = (DateTime)request.FechaFinDisfrute;
                        solicitudVacaciones.FechaRegreso = (DateTime)request.FechaFinDisfrute.Value.AddDays(1);
                        solicitudVacaciones.DiasDisfrute = solicitudVacaciones.DiasDisfrute - dias;
                    }
                }
                if (request.Justificacion != null)
                {
                    solicitudVacaciones.Justificacion = request.Justificacion;
                }

                contexto.SolicitudVacaciones.Update(solicitudVacaciones);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(solicitudVacaciones);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
