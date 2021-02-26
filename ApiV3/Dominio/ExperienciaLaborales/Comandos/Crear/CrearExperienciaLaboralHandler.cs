using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using ApiV3.Support;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ExperienciaLaborales.Comandos.Crear
{
    public class CrearExperienciaLaboralHandler : IRequestHandler<CrearExperienciaLaboralRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;

        public CrearExperienciaLaboralHandler(NominaDbContext contexto, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this.contexto = contexto;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }

        public async Task<CommandResult> Handle(CrearExperienciaLaboralRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var headers = httpContextAccessor.HttpContext.Request.Headers;
                var headKeyEsMovil = this.configuration.GetValue<string>(Constants.ClienteMovil.Key);
                var secretMovil = this.configuration.GetValue<string>(Constants.ClienteMovil.Value);

                ExperienciaLaboral experienciaLaboral = new ExperienciaLaboral
                {
                    FuncionarioId = request.FuncionarioId,
                    NombreCargo = Texto.TipoOracion(request.NombreCargo),
                    NombreEmpresa = request.NombreEmpresa,
                    Telefono = request.Telefono,
                    Salario = request.Salario,
                    NombreJefeInmediato = Texto.LetraCapital(request.NombreJefeInmediato),
                    FechaInicio = request.FechaInicio,
                    FechaFin = request.FechaFin,
                    FuncionesCargo = request.FuncionesCargo,
                    TrabajaActualmente = request.TrabajaActualmente,
                    MotivoRetiro = request.MotivoRetiro,
                    Observaciones = request.Observaciones
                };
                experienciaLaboral.Estado = EstadoInformacionFuncionario.Validado;
                if (headers[headKeyEsMovil].ToString() != null && headers[headKeyEsMovil].ToString() == secretMovil)
                {
                    experienciaLaboral.Estado = EstadoInformacionFuncionario.Pendiente;
                }
                this.contexto.ExperienciaLaborales.Add(experienciaLaboral);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(experienciaLaboral);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
