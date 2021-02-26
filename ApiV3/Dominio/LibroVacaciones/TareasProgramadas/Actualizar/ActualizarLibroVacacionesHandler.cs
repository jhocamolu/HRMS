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

namespace ApiV3.Dominio.LibroVacaciones.TareasProgramadas.Actualizar
{
    public class ActualizarLibroVacacionesHandler : IRequestHandler<ActualizarLibroVacacionesRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;

        public ActualizarLibroVacacionesHandler(NominaDbContext contexto, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this.contexto = contexto;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }

        public async Task<CommandResult> Handle(ActualizarLibroVacacionesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Obtener funcionario 
                string usuario = httpContextAccessor.HttpContext.Request.Headers.ContainsKey("JwtToken") ? InformacionToken.ObtenerInformacionUsuario(httpContextAccessor.HttpContext.Request.Headers["JwtToken"], "Identificacion") : "sistema";
                // Ejecuta el procedimiento almacenado
                var procedimientoAlcenado = await this.contexto.Database
                             .ExecuteSqlRawAsync($"EXEC [dbo].[USP_ActualizarLibroVacaciones] '{request.Fecha}','{usuario}','actualizar-libro-vacaciones'");
                return CommandResult.Success();

            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
