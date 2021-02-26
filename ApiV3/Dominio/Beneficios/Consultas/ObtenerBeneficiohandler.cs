using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Repositorio;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Beneficios.Consultas
{
    public class ObtenerBeneficiohandler : IRequestHandler<ObtenerBeneficioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        private readonly IReadOnlyRepository repositorio;

        public ObtenerBeneficiohandler(NominaDbContext contexto, IReadOnlyRepository repositorio)
        {
            this.contexto = contexto;
            this.repositorio = repositorio;
        }

        public async Task<CommandResult> Handle(ObtenerBeneficioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var beneficio = contexto.Beneficios.FirstOrDefault(x => x.Id == request.Id);
                if (beneficio == null)
                {
                    return CommandResult.Fail("No existe", 400);
                }

                var consulta = repositorio.Query($@"
                                    DECLARE @activo VARCHAR(255) = (SELECT estado.AUDITORIA_ACTIVO FROM util.VW_ConstanteEstado as estado)
                                    SELECT 
	                                    Beneficio.id as Beneficioid
	                                    ,Beneficio.TipoBeneficioId
	                                    ,Beneficio.FuncionarioId
                                        ,TipoBeneficioRequisito.id as TipoBeneficioRequisitoid
	                                    ,TipoSoporte.Nombre
	                                    ,BeneficioAdjunto.Id as BeneficioAdjuntoId
	                                    ,BeneficioAdjunto.Adjunto
                                    FROM Beneficio 
	                                    LEFT JOIN  TipoBeneficioRequisito ON Beneficio.TipoBeneficioId = TipoBeneficioRequisito.TipoBeneficioId
				                                    AND TipoBeneficioRequisito.EstadoRegistro =  @activo
	                                    LEFT JOIN TipoSoporte ON TipoBeneficioRequisito.TipoSoporteId = TipoSoporte.id
				                                    AND TipoSoporte.EstadoRegistro =  @activo
	                                    LEFT JOIN BeneficioAdjunto ON Beneficio.Id = BeneficioAdjunto.BeneficioId
				                                    AND BeneficioAdjunto.EstadoRegistro =  @activo
	                                    AND 	TipoBeneficioRequisito.id = BeneficioAdjunto.TipoBeneficioRequisitoId
				                                    AND TipoBeneficioRequisito.EstadoRegistro =  @activo
                                    WHERE Beneficio.id ='{request.Id}'").ToList();


                object objeto = new
                {
                    value = consulta.ToList()
                };

                return CommandResult.Success(objeto);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message, 500);
            }
        }
    }
}
