using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Graficas;
using ApiV3.Infraestructura.Repositorio;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Dashboard.Comandos.GraficasMovil
{
    public class GraficasMovilDashboardHandler : IRequestHandler<GraficasMovilDashboardRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IReadOnlyRepository repositorio;

        public GraficasMovilDashboardHandler(NominaDbContext contexto, IReadOnlyRepository repositorio, IGraficaServices graficas)
        {
            this.contexto = contexto;
            this.repositorio = repositorio;
        }

        public async Task<CommandResult> Handle(GraficasMovilDashboardRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Dictionary<string, object> objetos = new Dictionary<string, object>();

                var funcionario = contexto.Funcionarios.FirstOrDefault(x => x.Id.Equals(request.FuncionarioId));
                if (funcionario == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }

                var actualizacionDatos = repositorio
                                    .Query($"SELECT * FROM UFT_ObtenerPorcentajeActualizacionDatosFuncionario " +
                                           $"({request.FuncionarioId})"
                                    ).ToList();

                var permisos = repositorio
                                    .Query($"SELECT * FROM UFT_ObtenerCantidadPermisosPorEstadoFuncionario " +
                                           $"({request.FuncionarioId})"
                                    ).ToList();

                var beneficios = repositorio
                                    .Query($"SELECT * FROM UFT_ObtenerCantidadBeneficiosPorEstadoFuncionario " +
                                           $"({request.FuncionarioId})"
                                    ).ToList();



                objetos.Add("ActualizarDatos", actualizacionDatos);
                objetos.Add("Permiso", permisos);
                objetos.Add("Beneficios", beneficios);

                return CommandResult.Success(objetos);

            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
