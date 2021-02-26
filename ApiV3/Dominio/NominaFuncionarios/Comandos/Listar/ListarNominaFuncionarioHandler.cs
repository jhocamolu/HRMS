using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Repositorio;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.NominaFuncionarios.Comandos.Listar
{
    public class ListarNominaFuncionarioHandler : IRequestHandler<ListarNominaFuncionarioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IReadOnlyRepository repositorio;

        public ListarNominaFuncionarioHandler(NominaDbContext contexto, IReadOnlyRepository repository)
        {
            this.contexto = contexto;
            this.repositorio = repository;
        }

        public async Task<CommandResult> Handle(ListarNominaFuncionarioRequest request, CancellationToken cancellationToken)
        {
            try
            {

                var consulta = repositorio.Query($@"
                        DECLARE @CriterioBusqueda VARCHAR(255) = {(request.Funcionario == null ? "NULL" : "'" + request.Funcionario + "'")}
                        DECLARE @DependenciaId INT = {(request.DependenciaId == null ? "NULL" : "'" + request.DependenciaId + "'")}
                        DECLARE @CentroOperativoId INT = {(request.CentroOperativoId == null ? "NULL" : "'" + request.CentroOperativoId + "'")}
                        DECLARE @GrupoNominaId INT = {(request.GrupoNominaId == null ? "NULL" : "'" + request.GrupoNominaId + "'")}
                        SELECT * FROM [dbo].[UFT_ObtenerFuncionarioNomina]({request.NominaId})
                        WHERE (@CriterioBusqueda IS NULL OR (@CriterioBusqueda IS NOT NULL
                              AND CriterioBusqueda LIKE '%' + @CriterioBusqueda + '%'))
                             AND (@DependenciaId IS NULL OR (@DependenciaId IS NOT NULL
                                 AND DependenciaId = @DependenciaId))
	                         AND (@CentroOperativoId IS NULL OR (@CentroOperativoId IS NOT NULL
                                 AND CentroOperativoId = @CentroOperativoId))
                             AND (@GrupoNominaId IS NULL OR (@GrupoNominaId IS NOT NULL
                                 AND GrupoNominaId = @GrupoNominaId))").ToList();

                return CommandResult.Success(consulta);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
