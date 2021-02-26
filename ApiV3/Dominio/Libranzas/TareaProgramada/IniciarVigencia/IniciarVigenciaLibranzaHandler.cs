using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Libranzas.TareaProgramada.IniciarVigencia
{
    public class IniciarVigenciaLibranzaHandler : IRequestHandler<IniciarVigenciaLibranzaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor httpContextAccessor;

        public IniciarVigenciaLibranzaHandler(NominaDbContext contexto, IHttpContextAccessor httpContextAccessor)
        {
            this.contexto = contexto;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<CommandResult> Handle(IniciarVigenciaLibranzaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Obtener funcionario 
                string usuario = httpContextAccessor.HttpContext.Request.Headers.ContainsKey("JwtToken") ? InformacionToken.ObtenerInformacionUsuario(httpContextAccessor.HttpContext.Request.Headers["JwtToken"], "Identificacion") : "sistema";

                // Ejecuta el procedimiento almacenado
                var procedimientoAlmacenado = await this.contexto.Database
                             .ExecuteSqlRawAsync($"EXECUTE [dbo].[USP_IniciarVigenciaLibranza]  '{request.Fecha}','{usuario}','iniciar-libranza'");

                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
