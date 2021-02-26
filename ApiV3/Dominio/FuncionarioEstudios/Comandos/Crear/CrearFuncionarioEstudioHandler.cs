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

namespace ApiV3.Dominio.FuncionarioEstudios.Comandos.Crear
{
    public class CrearFuncionarioEstudioHandler : IRequestHandler<CrearFuncionarioEstudioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IConfiguration configuration;

        public CrearFuncionarioEstudioHandler(NominaDbContext contexto, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            this.contexto = contexto;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }

        public async Task<CommandResult> Handle(CrearFuncionarioEstudioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var headers = httpContextAccessor.HttpContext.Request.Headers;
                var headKeyEsMovil = this.configuration.GetValue<string>(Constants.ClienteMovil.Key);
                var secretMovil = this.configuration.GetValue<string>(Constants.ClienteMovil.Value);

                FuncionarioEstudio funcionarioEstudio = new FuncionarioEstudio();

                funcionarioEstudio.FuncionarioId = (int)request.FuncionarioId;
                funcionarioEstudio.NivelEducativoId = (int)request.NivelEducativoId;
                funcionarioEstudio.InstitucionEducativa = request.InstitucionEducativa;
                funcionarioEstudio.PaisId = (int)request.PaisId;
                funcionarioEstudio.ProfesionId = request.ProfesionId;
                funcionarioEstudio.AnioDeInicio = (DateTime)request.AnioDeInicio;
                funcionarioEstudio.AnioDeFin = request.AnioDeFin;
                funcionarioEstudio.EstadoEstudio = (EstadoEstudio)request.EstadoEstudio;
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

                Console.WriteLine(funcionarioEstudio);
                this.contexto.FuncionarioEstudios.Add(funcionarioEstudio);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(funcionarioEstudio);

            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
