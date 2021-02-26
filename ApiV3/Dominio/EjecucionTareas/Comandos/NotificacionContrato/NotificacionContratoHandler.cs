using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Repositorio;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.EjecucionTareas.Comandos.NotificacionContrato
{
    public class NotificacionContratoHandler : IRequestHandler<NotificacionContratoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IReadOnlyRepository repositorio;
        private readonly IHttpContextAccessor httpContextAccessor;

        public NotificacionContratoHandler(NominaDbContext contexto, IReadOnlyRepository repositorio, IHttpContextAccessor httpContextAccessor)
        {
            this.contexto = contexto;
            this.repositorio = repositorio;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<CommandResult> Handle(NotificacionContratoRequest request, CancellationToken cancellationToken)
        {
            try
            {

                string usuario = httpContextAccessor.HttpContext.Request.Headers.ContainsKey("JwtToken") ? InformacionToken.ObtenerInformacionUsuario(httpContextAccessor.HttpContext.Request.Headers["JwtToken"], "Identificacion") : "sistema";

                var procedimientoAlcenado = await this.contexto.Database
                             .ExecuteSqlRawAsync($"EXEC [dbo].[USP_NotificarVencimientoContrato] '{usuario}','notificar-vencimientorequisicion'");

                return CommandResult.Success();

            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
