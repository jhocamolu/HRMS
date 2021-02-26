using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Contratos.Comandos.Crear
{
    public class CrearContratosHandler : IRequestHandler<CrearContratosRequest, CommandResult>
    {
        public class AdministradoraContrato
        {
            public int? AdministradoraId { get; set; }
            public DateTime? FechaInicio { get; set; }
        }


        private readonly NominaDbContext contexto;
        public CrearContratosHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearContratosRequest request, CancellationToken cancellationToken)
        {
            try
            {


                Contrato contrato = new Contrato();

                // Consulta id para tipo contrato, termino indefinido
                var tipoContrato = contexto.TipoContratos.FirstOrDefault(x=> x.Nombre == "Término indefinido");
                if (request.TipoContratoId == tipoContrato.Id)
                {
                    //Consulta año por defecto
                    var annoVigencia = contexto.AnnoVigencias.FirstOrDefault(x => x.Estado == EstadoAnnoVigencia.Vigente);
                    //Consulta parametro fecha 
                    var consultaFechaFinalizacion = contexto.ParametroGenerales.FirstOrDefault(x=> x.Alias == "FechaContratoTerminoIndefinido" &&
                    x.AnnoVigenciaId == annoVigencia.Id);

                    request.FechaFinalizacion = DateTime.Parse(consultaFechaFinalizacion.Valor);
                }
                contrato.FuncionarioId = (int)request.FuncionarioId;
                contrato.TipoContratoId = (int)request.TipoContratoId;
                contrato.CargoDependenciaId = (int)request.CargoDependenciaId;
                contrato.PeriodoPrueba = (int)request.PeriodoPrueba;
                contrato.GrupoNominaId = (int)request.GrupoNominaId;
                contrato.FechaInicio = (DateTime)request.FechaInicio;
                contrato.FechaFinalizacion = request.FechaFinalizacion;
                contrato.Sueldo = (double)request.Sueldo;
                contrato.CentroOperativoId = (int)request.CentroOperativoId;
                contrato.DivisionPoliticaNivel2Id = (int)request.DivisionPoliticaNivel2Id;
                contrato.CentroCostoId = (int)request.CentroCostoId;
                contrato.FormaPagoId = (int)request.FormaPagoId;
                contrato.TipoMonedaId = (int)request.TipoMonedaId;
                if (request.EntidadFinancieraId != null) contrato.EntidadFinancieraId = request.EntidadFinancieraId;
                if (request.TipoCuentaId != null) contrato.TipoCuentaId = request.TipoCuentaId;
                if (request.NumeroCuenta != null) contrato.NumeroCuenta = request.NumeroCuenta;
                contrato.RecibeDotacion = (bool)request.RecibeDotacion;
                contrato.JornadaLaboralId = (int)request.JornadaLaboralId;
                contrato.EmpleadoConfianza = (bool)request.EmpleadoConfianza;
                contrato.ProcedimientoRetencion = (ProcedimientoRetenciones)request.ProcedimientoRetencion;
                contrato.Estado = EstadoContrato.Vigente;
                contrato.NumeroContrato = request.NumeroContrato;
                contrato.FechaTerminacion = null;
                contrato.CargoGrupoId = (int)request.CargoGrupoId;
                contrato.TipoPeriodoId = (int)request.TipoPeriodoId;
                contrato.TipoCotizanteSubtipoCotizanteId = (int)request.TipoCotizanteSubtipoCotizanteId;
                contrato.ColombianoEnElExterior = (bool)request.ColombianoEnElExterior;
                contrato.ExtranjeroNoObligadoACotizarAPension = (bool)request.ExtranjeroNoObligadoACotizarAPension;


                //Si la fecha de inicio es mayor a la fecha actual, se deja en estado Sininiciar
                if (request.FechaInicio > DateTime.Today)
                {
                    contrato.Estado = EstadoContrato.SinIniciar;
                }
                else if (request.FechaInicio <= DateTime.Today)
                {
                    contrato.Estado = EstadoContrato.Vigente;

                    //Proceso para Actualizamso el estado del funcionario.
                    Funcionario funcionarioEstado = this.contexto.Funcionarios.FirstOrDefault(x => x.Id == request.FuncionarioId);
                    funcionarioEstado.Estado = EstadoFuncionario.Activo;

                    this.contexto.Funcionarios.Update(funcionarioEstado);
                    await this.contexto.SaveChangesAsync();
                };

                this.contexto.Contratos.Add(contrato);
                await this.contexto.SaveChangesAsync();

                //Se almacena centro de trabajo
                ContratoCentroTrabajo contratoCentroTrabajo = new ContratoCentroTrabajo();
                contratoCentroTrabajo.CentroTrabajoId = (int)request.CentroTrabajoId;
                contratoCentroTrabajo.ContratoId = contrato.Id;
                contratoCentroTrabajo.FechaInicio = (DateTime)request.FechaInicio;
                this.contexto.ContratoCentroTrabajos.Add(contratoCentroTrabajo);
                await this.contexto.SaveChangesAsync();

                List<AdministradoraContrato> AdministradoraContrato = new List<AdministradoraContrato>();
                AdministradoraContrato.Add(new AdministradoraContrato
                {
                    AdministradoraId = request.Eps,
                    FechaInicio = request.EpsFechaInicio
                });

                AdministradoraContrato.Add(new AdministradoraContrato
                {
                    AdministradoraId = request.CajaCompensacion,
                    FechaInicio = request.CajaCompensacionFechaInicio
                });

                AdministradoraContrato.Add(new AdministradoraContrato
                {
                    AdministradoraId = request.FondoCesantias,
                    FechaInicio = request.FondoCesantiasFechaInicio
                });

                AdministradoraContrato.Add(new AdministradoraContrato
                {
                    AdministradoraId = request.Afp,
                    FechaInicio = request.AfPFechaInicio
                });

                //Proceso para guardar detalle en el modelo ContratoAdministradora
                int numeroItem = AdministradoraContrato.Count();
                int contador = 0;
                foreach (var item in AdministradoraContrato)
                {
                    contador++;
                    ContratoAdministradora contratoAdministradora = new ContratoAdministradora();

                    contratoAdministradora.ContratoId = contrato.Id;
                    contratoAdministradora.AdministradoraId = (int)item.AdministradoraId;
                    contratoAdministradora.FechaInicio = (DateTime)item.FechaInicio;

                    this.contexto.ContratoAdministradoras.Add(contratoAdministradora);
                    await this.contexto.SaveChangesAsync();

                    if (numeroItem == contador)
                    {
                        if (contratoAdministradora.Contrato.ContratoAdministradoras != null)
                        {
                            contratoAdministradora.Contrato.ContratoAdministradoras = null;
                        }
                    }
                }
                if (request.CargoDependenciaId != null)
                {
                    contrato.CargoDependencia = new CargoDependencia();
                }
                if (contrato.ContratoCentroTrabajos != null)
                {
                    contrato.ContratoCentroTrabajos = null;
                }

                return CommandResult.Success(contrato);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
