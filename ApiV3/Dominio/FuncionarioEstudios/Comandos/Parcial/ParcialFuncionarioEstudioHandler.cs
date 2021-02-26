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

namespace ApiV3.Dominio.FuncionarioEstudios.Comandos.Parcial
{
    public class ParcialFuncionarioEstudioHandler : IRequestHandler<ParcialFuncionarioEstudioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;

        public ParcialFuncionarioEstudioHandler(NominaDbContext contexto, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this.contexto = contexto;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }

        public async Task<CommandResult> Handle(ParcialFuncionarioEstudioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var headers = httpContextAccessor.HttpContext.Request.Headers;
                var headKeyEsMovil = this.configuration.GetValue<string>(Constants.ClienteMovil.Key);
                var secretMovil = this.configuration.GetValue<string>(Constants.ClienteMovil.Value);

                FuncionarioEstudio funcionarioEstudio = this.contexto.FuncionarioEstudios.Find(request.Id);

                if (request.FuncionarioId != null)
                {
                    funcionarioEstudio.FuncionarioId = (int)request.FuncionarioId;
                }

                if (request.NivelEducativoId != null)
                {
                    funcionarioEstudio.NivelEducativoId = (int)request.NivelEducativoId;
                }

                if (request.InstitucionEducativa != null)
                {
                    funcionarioEstudio.InstitucionEducativa = request.InstitucionEducativa;
                }

                if (request.PaisId != null)
                {
                    funcionarioEstudio.PaisId = (int)request.PaisId;
                }

                if (request.ProfesionId != null)
                {
                    funcionarioEstudio.ProfesionId = request.ProfesionId;
                }

                if (request.AnioDeInicio != null)
                {
                    funcionarioEstudio.AnioDeInicio = (DateTime)request.AnioDeInicio;
                }

                if (request.AnioDeFin != null)
                {
                    funcionarioEstudio.AnioDeFin = request.AnioDeFin;
                }

                if (request.EstadoEstudio != null)
                {
                    funcionarioEstudio.EstadoEstudio = (EstadoEstudio)request.EstadoEstudio;

                }

                if (request.TarjetaProfesional != null)
                {
                    funcionarioEstudio.TarjetaProfesional = request.TarjetaProfesional.ToUpper();
                }

                if (request.Titulo != null)
                {
                    funcionarioEstudio.Titulo = Texto.TipoOracion(request.Titulo);
                }

                if (request.Observacion != null)
                {
                    funcionarioEstudio.Observacion = request.Observacion;
                }

                if (request.Justificacion != null)
                {
                    funcionarioEstudio.Justificacion = request.Justificacion;
                }
                if (request.Estado != null)
                {
                    if (headers[headKeyEsMovil].ToString() == "" || headers[headKeyEsMovil].ToString() == null)
                    {
                        funcionarioEstudio.Estado = (EstadoInformacionFuncionario)request.Estado;
                    }
                }


                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        funcionarioEstudio.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        funcionarioEstudio.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }

                contexto.FuncionarioEstudios.Update(funcionarioEstudio);

                await contexto.SaveChangesAsync();
                return CommandResult.Success(funcionarioEstudio);

            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
