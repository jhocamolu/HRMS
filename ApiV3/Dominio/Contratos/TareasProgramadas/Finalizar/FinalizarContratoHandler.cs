using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Contratos.TareasProgramadas.Finalizar
{
    public class FinalizarContratoHandler : IRequestHandler<FinalizarContratoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor httpContextAccessor;

        public FinalizarContratoHandler(NominaDbContext contexto, IHttpContextAccessor httpContextAccessor)
        {
            this.contexto = contexto;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<CommandResult> Handle(FinalizarContratoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Obtener funcionario 
                string usuario = httpContextAccessor.HttpContext.Request.Headers.ContainsKey("JwtToken") ? InformacionToken.ObtenerInformacionUsuario(httpContextAccessor.HttpContext.Request.Headers["JwtToken"], "Identificacion") : "sistema";
                // Ejecuta el procedimiento almacenado
                var procedimientoAlcenado = await this.contexto.Database
                             .ExecuteSqlRawAsync($"EXEC [dbo].[USP_FinalizarContrato] '{request.Fecha}','{usuario}','finalizar-contrato'");

                return CommandResult.Success();

            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
