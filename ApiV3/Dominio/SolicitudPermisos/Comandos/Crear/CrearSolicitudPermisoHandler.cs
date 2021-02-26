using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using ApiV3.Support;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.SolicitudPermisos.Comandos.Crear
{
    public class CrearSolicitudPermisoHandler : IRequestHandler<CrearSolicitudPermisoRequest, CommandResult>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration configuration;
        private readonly NominaDbContext contexto;

        public CrearSolicitudPermisoHandler(NominaDbContext contexto, IHttpContextAccessor _httpContextAccessor, IConfiguration configuration)
        {
            this.contexto = contexto;
            this._httpContextAccessor = _httpContextAccessor;
            this.configuration = configuration;
        }

        public async Task<CommandResult> Handle(CrearSolicitudPermisoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var req = _httpContextAccessor.HttpContext.Request.Headers;

                SolicitudPermiso solicitudPermiso = new SolicitudPermiso();
                solicitudPermiso.FuncionarioId = (int)request.FuncionarioId;
                solicitudPermiso.TipoAusentismoId = (int)request.TipoAusentismoId;
                solicitudPermiso.FechaInicio = (DateTime)request.FechaInicio;
                solicitudPermiso.FechaFin = (DateTime)request.FechaFin;
                if (request.HoraLlegada != null)
                {
                    solicitudPermiso.HoraLlegada = (TimeSpan)request.HoraLlegada;
                }
                if (request.HoraSalida != null)
                {
                    solicitudPermiso.HoraSalida = (TimeSpan)request.HoraSalida;

                }
                solicitudPermiso.Observaciones = request.Observaciones;

                if (
                    req[this.configuration.GetValue<string>(Constants.ClienteMovil.Key)].ToString() != null
                    &&
                    req[this.configuration.GetValue<string>(Constants.ClienteMovil.Key)].ToString() == this.configuration.GetValue<string>(Constants.ClienteMovil.Value))
                {
                    solicitudPermiso.Estado = EstadoSolicitudPermiso.Solicitada;
                }
                else
                {
                    solicitudPermiso.Estado = EstadoSolicitudPermiso.Autorizada;
                }
                contexto.SolicitudPermisos.Add(solicitudPermiso);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(solicitudPermiso);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
