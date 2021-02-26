using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using ApiV3.Support;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.AusentismoFuncionarios.Comandos.Crear
{
    public class CrearAusentismoFuncionarioHandler : IRequestHandler<CrearAusentismoFuncionarioRequest, CommandResult>
    {
        public readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration configuration;

        public CrearAusentismoFuncionarioHandler(NominaDbContext contexto, IHttpContextAccessor _httpContextAccessor, IConfiguration configuration)
        {
            this.contexto = contexto;
            this._httpContextAccessor = _httpContextAccessor;
            this.configuration = configuration;
        }

        public async Task<CommandResult> Handle(CrearAusentismoFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var req = _httpContextAccessor.HttpContext.Request.Headers;

                AusentismoFuncionario ausentismoFuncionario = new AusentismoFuncionario();
                ausentismoFuncionario.FuncionarioId = request.FuncionarioId;
                ausentismoFuncionario.TipoAusentismoId = (int)request.TipoAusentismoId;
                ausentismoFuncionario.DiagnosticoCieId = request.DiagnosticoId;
                ausentismoFuncionario.FechaInicio = request.FechaInicio;
                ausentismoFuncionario.FechaFin = request.FechaFin;
                if (request.FechaIniciaReal != null)
                {
                    ausentismoFuncionario.FechaIniciaReal = (DateTime)request.FechaIniciaReal;
                }
                if (request.FechaFinalReal != null)
                {
                    ausentismoFuncionario.FechaFinalReal = (DateTime)request.FechaFinalReal;
                }
                ausentismoFuncionario.HoraInicio = request.HoraInicio;
                ausentismoFuncionario.HoraFin = request.HoraFin;

                if (
                    req[this.configuration.GetValue<string>(Constants.ClienteMovil.Key)].ToString() != null
                    &&
                    req[this.configuration.GetValue<string>(Constants.ClienteMovil.Key)].ToString() == this.configuration.GetValue<string>(Constants.ClienteMovil.Value))
                {
                    ausentismoFuncionario.Estado = EstadoAusentismo.Registrado;
                }
                else
                {
                    ausentismoFuncionario.Estado = EstadoAusentismo.Aprobado;
                }

                if (request.NumeroIncapacidad != null)
                {
                    ausentismoFuncionario.NumeroIncapacidad = request.NumeroIncapacidad;
                }
                if (request.Adjunto != null)
                {
                    ausentismoFuncionario.Adjunto = request.Adjunto;
                }
                if (request.Observacion != null)
                {
                    ausentismoFuncionario.Observacion = request.Observacion;
                }
                contexto.AusentismoFuncionarios.Add(ausentismoFuncionario);
                await contexto.SaveChangesAsync();

                if (request.ProrrogaId != null)
                {
                    ProrrogaAusentismo prorrogaAusentismo = new ProrrogaAusentismo();
                    prorrogaAusentismo.AusentismoId = ausentismoFuncionario.Id;
                    prorrogaAusentismo.ProrrogaId = (int)request.ProrrogaId;
                    contexto.ProrrogaAusentismos.Add(prorrogaAusentismo);
                    await contexto.SaveChangesAsync();
                }

                if (ausentismoFuncionario.AusentismoDe != null)
                {
                    ausentismoFuncionario.AusentismoDe = new List<ProrrogaAusentismo>();
                    ausentismoFuncionario.ProrrogaDe = new List<ProrrogaAusentismo>();
                }


                return CommandResult.Success(ausentismoFuncionario);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
