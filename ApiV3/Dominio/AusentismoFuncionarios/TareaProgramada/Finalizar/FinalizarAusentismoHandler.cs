using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.AusentismoFuncionarios.TareaProgramada.Finalizar
{
    public class FinalizarAusentismoHandler : IRequestHandler<FinalizarAusentismoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor httpContextAccessor;

        public FinalizarAusentismoHandler(NominaDbContext contexto, IHttpContextAccessor httpContextAccessor)
        {
            this.contexto = contexto;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<CommandResult> Handle(FinalizarAusentismoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Obtener funcionario 
                string usuario = httpContextAccessor.HttpContext.Request.Headers.ContainsKey("JwtToken") ? InformacionToken.ObtenerInformacionUsuario(httpContextAccessor.HttpContext.Request.Headers["JwtToken"], "Identificacion") : "sistema";

                // Ejecuta el procedimiento almacenado
                var procedimientoAlmacenado = await this.contexto.Database
                             .ExecuteSqlRawAsync($"EXECUTE [dbo].[USP_FinalizarAusentismo]  '{request.Fecha}','{usuario}','finalizar-ausentismo'");

                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
