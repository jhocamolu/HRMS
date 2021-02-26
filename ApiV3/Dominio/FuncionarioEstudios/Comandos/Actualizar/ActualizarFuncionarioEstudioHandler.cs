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

namespace ApiV3.Dominio.FuncionarioEstudios.Comandos.Actualizar
{
    public class ActualizarFuncionarioEstudioHandler : IRequestHandler<ActualizarFuncionarioEstudioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;

        public ActualizarFuncionarioEstudioHandler(NominaDbContext contexto, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this.contexto = contexto;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }

        public async Task<CommandResult> Handle(ActualizarFuncionarioEstudioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var headers = httpContextAccessor.HttpContext.Request.Headers;
                var headKeyEsMovil = this.configuration.GetValue<string>(Constants.ClienteMovil.Key);
                var secretMovil = this.configuration.GetValue<string>(Constants.ClienteMovil.Value);

                FuncionarioEstudio funcionarioEstudio = this.contexto.FuncionarioEstudios.Find(request.Id);

                funcionarioEstudio.FuncionarioId = request.FuncionarioId;
                funcionarioEstudio.NivelEducativoId = request.NivelEducativoId;
                funcionarioEstudio.InstitucionEducativa = request.InstitucionEducativa;
                funcionarioEstudio.PaisId = request.PaisId;
                funcionarioEstudio.ProfesionId = request.ProfesionId;
                funcionarioEstudio.AnioDeInicio = request.AnioDeInicio;
                funcionarioEstudio.AnioDeFin = request.AnioDeFin;
                funcionarioEstudio.EstadoEstudio = request.EstadoEstudio;
                if (request.TarjetaProfesional != null)
                {
                    funcionarioEstudio.TarjetaProfesional = request.TarjetaProfesional.ToUpper();
                }
                funcionarioEstudio.Titulo = Texto.TipoOracion(request.Titulo);
                funcionarioEstudio.Observacion = request.Observacion;

                funcionarioEstudio.Estado = EstadoInformacionFuncionario.Validado;
                if (headers[headKeyEsMovil].ToString() != null && headers[headKeyEsMovil].ToString() == secretMovil)
                {
                    funcionarioEstudio.Estado = EstadoInformacionFuncionario.Pendiente;
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
