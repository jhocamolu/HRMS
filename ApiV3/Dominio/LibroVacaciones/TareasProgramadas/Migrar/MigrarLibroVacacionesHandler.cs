using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.LibroVacaciones.TareasProgramadas.Migrar
{
    public class MigrarLibroVacacionesHandler : IRequestHandler<MigrarLibroVacacionesRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor httpContextAccessor;

        public MigrarLibroVacacionesHandler(NominaDbContext contexto, IHttpContextAccessor httpContextAccessor)
        {
            this.contexto = contexto;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<CommandResult> Handle(MigrarLibroVacacionesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                string usuario = httpContextAccessor.HttpContext.Request.Headers.ContainsKey("JwtToken") ? InformacionToken.ObtenerInformacionUsuario(httpContextAccessor.HttpContext.Request.Headers["JwtToken"], "Identificacion") : "sistema";
                var procedimientoAlcenado = await this.contexto.Database
                             .ExecuteSqlRawAsync($"EXEC [dbo].[USP_ActualizarLibroVacacionesTodos] '{request.Fecha}','{usuario}','migrar-libro-vacaciones'");
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
