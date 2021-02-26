using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.SolicitudVacaciones.TareasProgramadas.Actualizar
{
    public class ActualizarSolicitudVacacionesHandler : IRequestHandler<ActualizarSolicitudVacacionesRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;

        public ActualizarSolicitudVacacionesHandler(NominaDbContext contexto, IHttpContextAccessor _httpContextAccessor, IConfiguration configuration)
        {
            this.contexto = contexto;
            this.httpContextAccessor = _httpContextAccessor;
            this.configuration = configuration;
        }

        public async Task<CommandResult> Handle(ActualizarSolicitudVacacionesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Obtener funcionario 
                string usuario = httpContextAccessor.HttpContext.Request.Headers.ContainsKey("JwtToken") ? InformacionToken.ObtenerInformacionUsuario(httpContextAccessor.HttpContext.Request.Headers["JwtToken"], "Identificacion") : "sistema";

                // Ejecuta el procedimiento almacenado
                var procedimientoAlmacenado = await this.contexto.Database
                             .ExecuteSqlRawAsync($"EXECUTE [dbo].[USP_ActualizarSolicitudVacaciones]  '{request.Fecha}','{usuario}','actualizar-solicitud-vacaciones'");

                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
