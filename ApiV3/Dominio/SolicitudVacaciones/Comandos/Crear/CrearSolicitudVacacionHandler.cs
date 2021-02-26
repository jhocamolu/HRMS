using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using ApiV3.Support;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.SolicitudVacaciones.Comandos.Crear
{
    public class CrearSolicitudVacacionHandler : IRequestHandler<CrearSolicitudVacacionRequest, CommandResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration configuration;
        private readonly NominaDbContext contexto;

        public CrearSolicitudVacacionHandler(NominaDbContext contexto, IHttpContextAccessor _httpContextAccessor, IConfiguration configuration)
        {
            this.contexto = contexto;
            this._httpContextAccessor = _httpContextAccessor;
            this.configuration = configuration;
        }

        public async Task<CommandResult> Handle(CrearSolicitudVacacionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var req = _httpContextAccessor.HttpContext.Request.Headers;

                SolicitudVacacion solicitudVacacion = new SolicitudVacacion();
                solicitudVacacion.FuncionarioId = (int)request.FuncionarioId;
                solicitudVacacion.LibroVacacionesId = (int)request.LibroVacacionesId;
                solicitudVacacion.FechaInicioDisfrute = (DateTime)request.FechaInicioDisfrute;
                solicitudVacacion.DiasDisfrute = (int)request.DiasDisfrute;
                solicitudVacacion.FechaFinDisfrute = request.FechaFinDisfrute;
                solicitudVacacion.DiasDinero = (int)request.DiasDinero;
                solicitudVacacion.Observacion = request.Observacion;
                solicitudVacacion.FechaRegreso = request.FechaFinDisfrute.AddDays(1);
                solicitudVacacion.FechaPago = DateTime.Parse(request.FechaInicioDisfrute.ToString()).AddDays(-1);

                if (
                    req[this.configuration.GetValue<string>(Constants.ClienteMovil.Key)].ToString() != null
                    &&
                    req[this.configuration.GetValue<string>(Constants.ClienteMovil.Key)].ToString() == this.configuration.GetValue<string>(Constants.ClienteMovil.Value))
                {
                    solicitudVacacion.Estado = EstadoSolicitudVacaciones.Solicitada;
                }
                else
                {
                    solicitudVacacion.Estado = EstadoSolicitudVacaciones.Autorizada;
                }

                contexto.SolicitudVacaciones.Add(solicitudVacacion);
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
