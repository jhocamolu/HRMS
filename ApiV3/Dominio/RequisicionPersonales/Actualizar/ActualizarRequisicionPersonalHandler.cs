using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.RequisicionPersonales.Actualizar
{
    public class ActualizarRequisicionPersonalHandler : IRequestHandler<ActualizarRequisicionPersonalRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarRequisicionPersonalHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarRequisicionPersonalRequest request, CancellationToken cancellationToken)
        {
            try
            {
                RequisicionPersonal requisicion = await contexto.RequisicionPersonales.FindAsync(request.Id);
                requisicion.CargoDependenciaSolicitanteId = (int)request.CargoDependenciaSolicitanteId;
                requisicion.FuncionarioSolicitanteId = (int)request.FuncionarioSolicitanteId;
                requisicion.CargoDependenciaSolicitadoId = (int)request.CargoDependenciaSolicitadoId;
                requisicion.DivisionPoliticaNivel2Id = (int)request.DivisionPoliticaNivel2Id;
                requisicion.Cantidad = (int)request.Cantidad;
                requisicion.TipoContratoId = (int)request.TipoContratoId;
                requisicion.CentroCostoId = (int)request.CentroCostoId;
                requisicion.FechaInicio = (DateTime)request.FechaInicio;
                requisicion.MotivoVacanteId = (int)request.MotivoVacanteId;
                requisicion.PerfilCargo = request.PerfilCargo;
                requisicion.CompetenciaCargo = request.CompetenciaCargo;
                requisicion.TipoReclutamiento = request.TipoReclutamiento;

                if (request.CentroOperativoSolicitanteId != null)
                {
                    requisicion.CentroOperativoSolicitanteId = request.CentroOperativoSolicitanteId;
                }
                if (request.CentroOperativoSolicitadoId != null)
                {
                    requisicion.CentroOperativoSolicitadoId = request.CentroOperativoSolicitadoId;
                }
                if (request.FechaFin != null)
                {
                    requisicion.FechaFin = (DateTime)request.FechaFin;
                }
                if (request.FuncionarioAQuienReemplazaId != null)
                {
                    requisicion.FuncionarioAQuienReemplazaId = (int)request.FuncionarioAQuienReemplazaId;
                }
                if (request.Salario != null)
                {
                    requisicion.Salario = request.Salario;
                }
                if (request.SalarioPortalReclutamiento != null)
                {
                    requisicion.SalarioPortalReclutamiento = (bool)request.SalarioPortalReclutamiento;
                }
                if (request.PerfilPortalReclutamiento != null)
                {
                    requisicion.PerfilPortalReclutamiento = (bool)request.PerfilPortalReclutamiento;
                }
                if (request.CompetenciaPortalReclutamiento != null)
                {
                    requisicion.CompetenciaPortalReclutamiento = (bool)request.CompetenciaPortalReclutamiento;
                }
                if (request.Observacion != null)
                {
                    requisicion.Observacion = request.Observacion;
                }


                this.contexto.RequisicionPersonales.Update(requisicion);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(requisicion);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
;