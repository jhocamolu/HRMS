using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Repositorio;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.NominaFuncionarios.Comandos.Iniciar
{
    public class IniciarNominaFuncionarioHandler : IRequestHandler<IniciarNominaFuncionarioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IReadOnlyRepository repository;

        public IniciarNominaFuncionarioHandler(NominaDbContext contexto, IHttpContextAccessor httpContextAccessor, IReadOnlyRepository repository)
        {
            this.contexto = contexto;
            this.httpContextAccessor = httpContextAccessor;
            this.repository = repository;
        }

        public async Task<CommandResult> Handle(IniciarNominaFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                List<NominaFuncionario> lista = new List<NominaFuncionario>();

                Funcionario funcionario = InformacionToken.ObtenerInformacionFuncionario(httpContextAccessor.HttpContext.Request.Headers["JwtToken"], contexto);

                string idJoin = request.NominaFuncionario.Count > 0 ? $"{String.Join(",", request.NominaFuncionario)}" : "";
                string sql = $"UPDATE dbo.NominaFuncionario SET Estado = (SELECT vce.NOMINAFUNCIONARIO_PENDIENTELIQUIDACION FROM util.VW_ConstanteEstado vce)  WHERE Id IN ({idJoin})";
                await contexto.Database.ExecuteSqlCommandAsync(sql);

                lista = contexto.NominaFuncionarios.Where(z => request.NominaFuncionario.Contains(z.Id)).ToList();

                int UsuarioId = funcionario.Id;
                string UsuarioNombre = funcionario.NumeroDocumento;

                string query = $@"EXECUTE [dbo].[USP_CalcularNomina] {request.NominaId}, '{UsuarioId}', '{UsuarioNombre}'";

                try
                {
                    this.repository.NonQuery(query);
                }
                catch (Exception e)
                {
                    return CommandResult.Fail(e.Message, 400);
                }
                return CommandResult.Success(lista);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
