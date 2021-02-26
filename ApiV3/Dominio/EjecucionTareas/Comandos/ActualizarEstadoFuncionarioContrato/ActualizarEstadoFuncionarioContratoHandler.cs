using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.EjecucionTareas.Comandos.ActualizarEstadoFuncionarioContrato
{
    public class ActualizarEstadoFuncionarioContratoHandler : IRequestHandler<ActualizarEstadoFuncionarioContratoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ActualizarEstadoFuncionarioContratoHandler(NominaDbContext contexto, IHttpContextAccessor httpContextAccessor)
        {
            this.contexto = contexto;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<CommandResult> Handle(ActualizarEstadoFuncionarioContratoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // Obtener funcionario 
                string usuario = httpContextAccessor.HttpContext.Request.Headers.ContainsKey("JwtToken") ? InformacionToken.ObtenerInformacionUsuario(httpContextAccessor.HttpContext.Request.Headers["JwtToken"], "Identificacion") : "sistema";

                // Ejecuta el procedimiento almacenado
                var procedimientoAlmacenado = await this.contexto.Database
                             .ExecuteSqlRawAsync($"DECLARE @Estado varchar(255);DECLARE @Resultado varchar(255); EXECUTE [dbo].[USP_ProcesarEstadosFuncionario]  '{request.Fecha}','{usuario}','actualizar-estadofuncionariocontrato',@Estado OUTPUT, @Resultado OUTPUT; SELECT @Estado, @Resultado; ");

                return CommandResult.Success();

            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }


    }
}
