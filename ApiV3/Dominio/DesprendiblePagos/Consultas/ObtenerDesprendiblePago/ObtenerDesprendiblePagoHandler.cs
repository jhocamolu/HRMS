using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Repositorio;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.DesprendiblePagos.Consultas.ObtenerDesprendiblePago
{
    public class ObtenerDesprendiblePagoHandler : IRequestHandler<ObtenerDesprendiblePagoRequest, CommandResult>
    {
        private readonly NominaDbContext context;
        const string PARAMETRO = "cantidadPeriodoDesprendible";
        private readonly IReadOnlyRepository repositorio;

        public ObtenerDesprendiblePagoHandler(NominaDbContext context, IReadOnlyRepository repositorio)
        {
            this.context = context;
            this.repositorio = repositorio;
        }

        public async Task<CommandResult> Handle(ObtenerDesprendiblePagoRequest request, CancellationToken cancellationToken)
        {
            try
            {

                var consulta = repositorio.Query($@"SET LANGUAGE spanish
SELECT TOP(SELECT CONVERT(INT, util.UFS_ObtenerParametro('cantidadPeriodoDesprendible',NULL)))
nf.Id AS 'NominaFuncionarioId',
nf.FuncionarioId,
YEAR(n.FechaFinal) AS 'Anio',
DATENAME(MONTH, n.FechaFinal) AS 'Mes',
s.Nombre AS 'Subperiodo'
FROM NominaFuncionario nf
     INNER JOIN Nomina n ON nf.NominaId = n.Id
                            AND n.Estado =
(
    SELECT vce.[NOMINA_APLICADA]
    FROM util.VW_ConstanteEstado vce
)
                            AND nf.Estado =
(
    SELECT vce.NOMINAFUNCIONARIO_LIQUIDADO
    FROM util.VW_ConstanteEstado vce
)
                            AND nf.EstadoRegistro =
(
    SELECT vce.AUDITORIA_ACTIVO
    FROM util.VW_ConstanteEstado vce
)
                            AND n.EstadoRegistro =
(
    SELECT vce.AUDITORIA_ACTIVO
    FROM util.VW_ConstanteEstado vce
)
     INNER JOIN Subperiodo s ON n.SubperiodoId = s.Id
                                AND s.EstadoRegistro =
(
    SELECT vce.AUDITORIA_ACTIVO
    FROM util.VW_ConstanteEstado vce
)
WHERE nf.FuncionarioId = {request.FuncionarioId}
ORDER BY n.FechaFinal DESC;");
                if (!consulta.Any())
                {
                    return CommandResult.Fail("El funcionario no posee desprendibles actualmente.");
                }

                object objeto = new
                {
                    value = consulta.ToList()
                };

                return CommandResult.Success(objeto);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
