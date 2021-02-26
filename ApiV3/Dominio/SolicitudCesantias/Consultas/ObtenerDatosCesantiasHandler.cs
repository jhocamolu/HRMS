using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Repositorio;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.SolicitudCesantias.Consultas
{
    public class ObtenerDatosCesantiasHandler : IRequestHandler<ObtenerDatosCesantiasRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IReadOnlyRepository repositorio;

        public ObtenerDatosCesantiasHandler(NominaDbContext contexto, IReadOnlyRepository repositorio)
        {
            this.contexto = contexto;
            this.repositorio = repositorio;
        }

        public async Task<CommandResult> Handle(ObtenerDatosCesantiasRequest request, CancellationToken cancellationToken)
        {
            try
            {
                dynamic result = "";
                // Consulta BaseCalculoCesantia promedio.
                double BaseCesantias = 0;
                double ValorCesantiasAcumuladas = 0;
                int CantidadDiasAcumulados = 0;
                double ValorInteresCesantiasAcumuladas = 0;
                var consultarProcedure = repositorio.Query($@"SELECT * FROM [dbo].[UFT_ObtenerDatosCesantias] ({(int)request.FuncionarioId})");
                var resultadoProcedimiento = consultarProcedure.First();

                if (consultarProcedure.Any())
                {
                    BaseCesantias = resultadoProcedimiento.BaseCesantias;
                    ValorCesantiasAcumuladas = resultadoProcedimiento.ValorCesantiasAcumuladas;
                    CantidadDiasAcumulados = resultadoProcedimiento.DiasAcumulados;
                    ValorInteresCesantiasAcumuladas = resultadoProcedimiento.ValorIntresesCesantiasAcumuladas;
                }

                // Consulta anticipos solicitados
                var anticiposCesantias = contexto.SolicitudCesantias.Where(x => x.FuncionarioId == request.FuncionarioId &&
                                                                                   (x.Estado == EstadoCesantia.Aprobada ||
                                                                                   x.Estado == EstadoCesantia.Finalizada))
                                                                        .GroupBy(x => x.FuncionarioId)
                                                                        .Select(x => x.Sum(i => i.ValorSolicitado))
                                                                        .FirstOrDefault();

                result = new
                {
                    baseCesantias = BaseCesantias,
                    valorCesantiasAcumuladas = ValorCesantiasAcumuladas,
                    cantidadDiasAcumulados = CantidadDiasAcumulados,
                    valorInteresCesantiasAcumuladas = ValorInteresCesantiasAcumuladas,
                    anticiposSolicitados = anticiposCesantias

                };

                return CommandResult.Success(result);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message, 500);
            }
        }
    }
}
