using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Repositorio;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Nominas.Comandos.Aprobar
{
    public class AprobarNominaHandler : IRequestHandler<AprobarNominaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IReadOnlyRepository repository;

        public AprobarNominaHandler(NominaDbContext contexto, IHttpContextAccessor httpContextAccessor, IReadOnlyRepository repository)
        {
            this.contexto = contexto;
            this.httpContextAccessor = httpContextAccessor;
            this.repository = repository;
        }

        public async Task<CommandResult> Handle(AprobarNominaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Funcionario funcionario = InformacionToken.ObtenerInformacionFuncionario(httpContextAccessor.HttpContext.Request.Headers["JwtToken"], contexto);

                int UsuarioId = funcionario.Id;

                string query = $@"EXECUTE [dbo].[USP_AprobarNomina] {request.Id}, '{UsuarioId}'";
                try
                {
                    this.repository.NonQuery(query);
                }
                catch (Exception e)
                {
                    return CommandResult.Fail(e.Message, 400);
                }

                var nomina = await contexto.Nominas.FirstOrDefaultAsync(x => x.Id == request.Id);

                return CommandResult.Success(nomina);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
