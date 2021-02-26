using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Funcionarios.Consulta
{
    public class ObtenerFuncionarioDatosActualesIdHandler : IRequestHandler<ObtenerFuncionarioDatosActualesIdRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ObtenerFuncionarioDatosActualesIdHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ObtenerFuncionarioDatosActualesIdRequest request, CancellationToken cancellationToken)
        {
            try
            {

                var contrato = this.contexto.Contratos
                                .Where(x => x.FuncionarioId == request.FuncionarioId)
                                .OrderByDescending(c => c.FechaInicio)
                                .FirstOrDefault();
                if (contrato != null)
                {
                    var otrosi = this.contexto.ContratoOtroSis
                               .Where(x => x.ContratoId == contrato.Id)
                               .OrderByDescending(c => c.Id)
                               .FirstOrDefault();
                    if (otrosi != null)
                    {
                        var centroOperativo = await this.contexto.CentroOperativos.FindAsync(otrosi.CentroOperativoId);
                        var cargoDependencia = this.contexto.CargoDependencias.Find(otrosi.CargoDependenciaId);
                        var dependencia = this.contexto.Dependencias.Find(cargoDependencia.DependenciaId);
                        var cargo = this.contexto.Cargos.Find(cargoDependencia.CargoId);

                        dynamic result = new
                        {
                            centroOperativoId = centroOperativo.Id,
                            centroOperativo = centroOperativo,
                            otrosi.CargoDependenciaId,
                            dependencia = dependencia,
                            cargo = cargo
                        };
                        return CommandResult.Success(result);
                    }
                    else
                    {
                        var centroOperativo = this.contexto.CentroOperativos.Find(contrato.CentroOperativoId);
                        var cargoDependencia = this.contexto.CargoDependencias.Find(contrato.CargoDependenciaId);
                        var dependencia = this.contexto.Dependencias.Find(cargoDependencia.DependenciaId);
                        var cargo = this.contexto.Cargos.Find(cargoDependencia.CargoId);

                        dynamic result = new
                        {
                            centroOperativoId = centroOperativo.Id,
                            centroOperativo = centroOperativo,
                            contrato.CargoDependenciaId,
                            dependencia = dependencia,
                            cargo = cargo
                        };
                        return CommandResult.Success(result);
                    }
                }
                else
                {
                    dynamic result = new
                    {
                        centroOperativoId = "",
                        centroOperativo = "",
                        CargoDependenciaId = "",
                        dependencia = "",
                        cargo = "",
                    };
                    return CommandResult.Success(result);
                }
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message, 500);
            }
            throw new NotImplementedException();
        }
    }
}
