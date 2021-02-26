using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Contratos.Comandos.Parcial
{
    public class ParcialContratoHandler : IRequestHandler<ParcialContratoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialContratoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }



        public async Task<CommandResult> Handle(ParcialContratoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Contrato contrato = await this.contexto.Contratos.FindAsync(request.Id);
                if (request.PeriodoPrueba != null) contrato.PeriodoPrueba = (int)request.PeriodoPrueba;
                if (request.CentroCostoId != null) contrato.CentroCostoId = (int)request.CentroCostoId;
                if (request.FormaPagoId != null) contrato.FormaPagoId = (int)request.FormaPagoId;
                if (request.TipoMonedaId != null) contrato.TipoMonedaId = (int)request.TipoMonedaId;
                if (request.GrupoNominaId != null) contrato.GrupoNominaId = (int)request.GrupoNominaId;
                if (request.EntidadFinancieraId != null) contrato.EntidadFinancieraId = request.EntidadFinancieraId;
                if (request.TipoCuentaId != null) contrato.TipoCuentaId = request.TipoCuentaId;
                if (request.NumeroCuenta != null) contrato.NumeroCuenta = request.NumeroCuenta;
                if (request.RecibeDotacion != null) contrato.RecibeDotacion = (bool)request.RecibeDotacion;
                if (request.JornadaLaboralId != null) contrato.JornadaLaboralId = (int)request.JornadaLaboralId;
                if (request.EmpleadoConfianza != null) contrato.EmpleadoConfianza = (bool)request.EmpleadoConfianza;
                if (request.ProcedimientoRetencion != null) contrato.ProcedimientoRetencion = (ProcedimientoRetenciones)request.ProcedimientoRetencion;
                if (request.Observaciones != null) contrato.Observaciones = Texto.TipoOracion(request.Observaciones);
                if (request.Justificacion != null) contrato.Justificacion = Texto.TipoOracion(request.Justificacion);
                if (request.Cancelar != null && request.Cancelar == true && contrato.Estado == EstadoContrato.SinIniciar)
                {
                    contrato.Estado = EstadoContrato.Cancelado;
                    ActualizaEstadoFuncionarioRetirado((int)contrato.FuncionarioId);
                }
                contrato.FechaTerminacion = null;
                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        contrato.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        contrato.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }

                this.contexto.Contratos.Update(contrato);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(contrato);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }

        private void ActualizaEstadoFuncionarioRetirado(int funcionarioId)
        {
            Funcionario funcionario = contexto.Funcionarios.Find(funcionarioId);
            funcionario.Estado = EstadoFuncionario.Retirado;

            contexto.Funcionarios.Update(funcionario);
            contexto.SaveChanges();
        }
    }
}
