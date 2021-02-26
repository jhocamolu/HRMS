using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.RequisicionPersonales.Crear
{
    public class CrearRequisicionPersonalHandler : IRequestHandler<CrearRequisicionPersonalRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearRequisicionPersonalHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearRequisicionPersonalRequest request, CancellationToken cancellationToken)
        {
            try
            {
                RequisicionPersonal requisicion = new RequisicionPersonal { };
                requisicion.CargoDependenciaSolicitanteId = (int)request.CargoDependenciaSolicitanteId;
                requisicion.CargoDependenciaSolicitadoId = (int)request.CargoDependenciaSolicitadoId;
                requisicion.FuncionarioSolicitanteId = (int)request.FuncionarioSolicitanteId;
                requisicion.DivisionPoliticaNivel2Id = (int)request.DivisionPoliticaNivel2Id;
                requisicion.Cantidad = (int)request.Cantidad;
                requisicion.TipoContratoId = (int)request.TipoContratoId;
                requisicion.CentroCostoId = (int)request.CentroCostoId;
                requisicion.FechaInicio = (DateTime)request.FechaInicio;
                requisicion.MotivoVacanteId = (int)request.MotivoVacanteId;
                requisicion.PerfilCargo = request.PerfilCargo;
                requisicion.CompetenciaCargo = request.CompetenciaCargo;
                requisicion.Estado = EstadoRequisicionPersonal.Solicitada;

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
                if (request.TipoReclutamiento != null)
                {
                    requisicion.TipoReclutamiento = (TipoReclutamiento)request.TipoReclutamiento;
                }
                if (request.Salario != null)
                {
                    requisicion.Salario = request.Salario;
                }
                if (request.SalarioPortalReclutamiento != null)
                {
                    requisicion.SalarioPortalReclutamiento = request.SalarioPortalReclutamiento;
                }
                if (request.PerfilPortalReclutamiento != null)
                {
                    requisicion.PerfilPortalReclutamiento = request.PerfilPortalReclutamiento;
                }
                if (request.CompetenciaPortalReclutamiento != null)
                {
                    requisicion.CompetenciaPortalReclutamiento = request.CompetenciaPortalReclutamiento;
                }
                if (request.Observacion != null)
                {
                    requisicion.Observacion = request.Observacion;
                }


                this.contexto.RequisicionPersonales.Add(requisicion);
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
