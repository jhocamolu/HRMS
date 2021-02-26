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

namespace ApiV3.Dominio.SolicitudVacaciones.TareasProgramadas.ActualizarInterrupcion
{
    public class ActualizarSolicitudVacacionesInterrupcionHandler : IRequestHandler<ActualizarSolicitudVacacionesInterrupcionRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;

        public ActualizarSolicitudVacacionesInterrupcionHandler(NominaDbContext contexto, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this.contexto = contexto;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }

        public async Task<CommandResult> Handle(ActualizarSolicitudVacacionesInterrupcionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Obtener funcionario 
                string usuario = httpContextAccessor.HttpContext.Request.Headers.ContainsKey("JwtToken") ? InformacionToken.ObtenerInformacionUsuario(httpContextAccessor.HttpContext.Request.Headers["JwtToken"], "Identificacion") : "sistema";

                var procedimientoAlmacenadoInterrupcion = await this.contexto.Database
                           .ExecuteSqlRawAsync($"EXEC [dbo].[USP_ActualizarSolicitudVacacionesInterrupcion] '{request.Fecha}','{usuario}','actualizar-solicitud-vacaciones-interrupcion'");

                return CommandResult.Success();

            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
