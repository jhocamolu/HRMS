using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Contratos.Consultas
{
    /// <summary>
    /// Clase encargada de realizar las consulta de los datos actuales de empleado, para
    /// Crear un otrosi.
    /// Se valida si existe un otrosi, deser así,  se envian los datos del ultimo otrosi.
    /// Si no existe un otroSi, se envian los datos desde el contrato
    /// </summary>

    public class ObtenerContratoDatosActualesHandler : IRequestHandler<ObtenerContratoDatosActualesRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ObtenerContratoDatosActualesHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ObtenerContratoDatosActualesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var otrosi = this.contexto.ContratoOtroSis
                                .Where(x => x.ContratoId == request.ContratoId)
                                .OrderByDescending(c => c.FechaAplicacion)
                                .FirstOrDefault();


                int valorMunicipio = 0;
                int identificador;
                int valorCentroOperativo = 0;
                int valorCargoDependencia = 0;
                int valorTipoContrato = 0;
                double sueldo;
                var observacion = "";
                DateTime fechaFinalizacion;
                if (otrosi != null)
                {
                    identificador = otrosi.Id;
                    valorMunicipio = otrosi.DivisionPoliticaNivel2Id;
                    valorCentroOperativo = otrosi.CentroOperativoId;
                    valorCargoDependencia = otrosi.CargoDependenciaId;
                    valorTipoContrato = otrosi.TipoContratoId;
                    fechaFinalizacion = (DateTime)otrosi.FechaFinalizacion;
                    sueldo = otrosi.Sueldo;
                    observacion = otrosi.Observaciones;

                }
                else
                {
                    var contrato = this.contexto.Contratos.Find(request.ContratoId);
                    identificador = contrato.Id;
                    valorMunicipio = contrato.DivisionPoliticaNivel2Id;
                    valorCentroOperativo = contrato.CentroOperativoId;
                    valorCargoDependencia = contrato.CargoDependenciaId;
                    valorTipoContrato = contrato.TipoContratoId;
                    fechaFinalizacion = (DateTime)contrato.FechaFinalizacion;
                    sueldo = contrato.Sueldo;
                    observacion = contrato.Observaciones;
                }


                var municipio = await this.contexto.DivisionPoliticaNiveles2.FindAsync(valorMunicipio);
                var departamento = this.contexto.DivisionPoliticaNiveles1.Find(municipio.DivisionPoliticaNivel1Id);
                var centroOperativo = this.contexto.CentroOperativos.Find(valorCentroOperativo);
                var cargoDependencia = this.contexto.CargoDependencias.Find(valorCargoDependencia);
                var cargo = this.contexto.Cargos.Find(cargoDependencia.CargoId);
                var dependencia = this.contexto.Dependencias.Find(cargoDependencia.DependenciaId);
                var tipoContrato = this.contexto.TipoContratos.Find(valorTipoContrato);

                dynamic result = new
                {
                    identificador = identificador,
                    valorTipoContrato,
                    TipoContrato = tipoContrato,
                    fechaFinalizacion,
                    valorCargoDependencia,
                    cargo = cargo,
                    dependencia = dependencia,
                    sueldo,
                    valorCentroOperativo,
                    centroOperativo = centroOperativo,
                    divisionPoliticaNivel2Id = municipio.Id,
                    divisionPoliticaNivel2 = municipio,
                    divisionPoliticaNivel1Id = departamento.Id,
                    divisionPoliticaNivel1 = departamento,
                    observacion = observacion
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
