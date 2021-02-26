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

namespace ApiV3.Dominio.ExperienciaLaborales.Comandos.Actualizar
{
    public class ActualizarExperienciaLaboralHandler : IRequestHandler<ActualizarExperienciaLaboralRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;

        public ActualizarExperienciaLaboralHandler(NominaDbContext contexto, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this.contexto = contexto;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }

        public async Task<CommandResult> Handle(ActualizarExperienciaLaboralRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var headers = httpContextAccessor.HttpContext.Request.Headers;
                var headKeyEsMovil = this.configuration.GetValue<string>(Constants.ClienteMovil.Key);
                var secretMovil = this.configuration.GetValue<string>(Constants.ClienteMovil.Value);

                ExperienciaLaboral experienciaLaboral = contexto.ExperienciaLaborales.Find(request.Id);
                experienciaLaboral.FuncionarioId = request.FuncionarioId;
                experienciaLaboral.NombreCargo = Texto.TipoOracion(request.NombreCargo);
                experienciaLaboral.NombreEmpresa = request.NombreEmpresa;
                experienciaLaboral.Telefono = request.Telefono;
                experienciaLaboral.Salario = request.Salario;
                experienciaLaboral.NombreJefeInmediato = Texto.LetraCapital(request.NombreJefeInmediato);
                experienciaLaboral.FechaInicio = request.FechaInicio;
                experienciaLaboral.FechaFin = request.FechaFin;
                experienciaLaboral.FuncionesCargo = request.FuncionesCargo;
                experienciaLaboral.TrabajaActualmente = request.TrabajaActualmente;
                experienciaLaboral.MotivoRetiro = request.MotivoRetiro;
                experienciaLaboral.Observaciones = request.Observaciones;

                experienciaLaboral.Estado = EstadoInformacionFuncionario.Validado;
                if (headers[headKeyEsMovil].ToString() != null && headers[headKeyEsMovil].ToString() == secretMovil)
                {
                    experienciaLaboral.Estado = EstadoInformacionFuncionario.Pendiente;
                }

                contexto.ExperienciaLaborales.Update(experienciaLaboral);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(experienciaLaboral);
            }

            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
