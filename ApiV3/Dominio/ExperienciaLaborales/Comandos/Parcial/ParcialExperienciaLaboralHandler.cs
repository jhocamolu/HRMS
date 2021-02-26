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

namespace ApiV3.Dominio.ExperienciaLaborales.Comandos.Parcial
{
    public class ParcialExperienciaLaboralHandler : IRequestHandler<ParcialExperienciaLaboralRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;

        public ParcialExperienciaLaboralHandler(NominaDbContext contexto, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this.contexto = contexto;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }

        public async Task<CommandResult> Handle(ParcialExperienciaLaboralRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var headers = httpContextAccessor.HttpContext.Request.Headers;
                var headKeyEsMovil = this.configuration.GetValue<string>(Constants.ClienteMovil.Key);
                var secretMovil = this.configuration.GetValue<string>(Constants.ClienteMovil.Value);

                ExperienciaLaboral experienciaLaboral = contexto.ExperienciaLaborales.Find(request.Id);

                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        experienciaLaboral.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        experienciaLaboral.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                if (request.FuncionarioId != null)
                {
                    experienciaLaboral.FuncionarioId = (int)request.FuncionarioId;
                }
                if (request.NombreCargo != null)
                {
                    experienciaLaboral.NombreCargo = Texto.TipoOracion(request.NombreCargo);
                }
                if (request.NombreEmpresa != null)
                {
                    experienciaLaboral.NombreEmpresa = request.NombreEmpresa;
                }
                if (request.Telefono != null)
                {
                    experienciaLaboral.Telefono = request.Telefono;
                }
                if (request.Salario != null)
                {
                    experienciaLaboral.Salario = request.Salario;
                }
                if (request.NombreJefeInmediato != null)
                {
                    experienciaLaboral.NombreJefeInmediato = Texto.LetraCapital(request.NombreJefeInmediato);
                }
                if (request.FechaInicio != null)
                {
                    experienciaLaboral.FechaInicio = (DateTime)request.FechaInicio;
                }
                if (request.FechaFin != null)
                {
                    experienciaLaboral.FechaFin = request.FechaFin;
                }
                if (request.FuncionesCargo != null)
                {
                    experienciaLaboral.FuncionesCargo = request.FuncionesCargo;
                }
                if (request.TrabajaActualmente != null)
                {
                    experienciaLaboral.TrabajaActualmente = request.TrabajaActualmente;
                }
                if (request.MotivoRetiro != null)
                {
                    experienciaLaboral.MotivoRetiro = request.MotivoRetiro;
                }
                if (request.Observaciones != null)
                {
                    experienciaLaboral.Observaciones = request.Observaciones;
                }
                if (request.Justificacion != null)
                {
                    experienciaLaboral.Justificacion = request.Justificacion;
                }
                if (request.Estado != null)
                {
                    if (headers[headKeyEsMovil].ToString() == "" || headers[headKeyEsMovil].ToString() == null)
                    {
                        experienciaLaboral.Estado = (EstadoInformacionFuncionario)request.Estado;
                    }
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
