using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Contratos.Comandos.Actualizar
{
    public class ActualizarContratoHandler : IRequestHandler<ActualizarContratoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ActualizarContratoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarContratoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Contrato contrato = await this.contexto.Contratos.FindAsync(request.Id);

                contrato.PeriodoPrueba = (int)request.PeriodoPrueba;
                contrato.CentroCostoId = request.CentroCostoId;
                contrato.FormaPagoId = request.FormaPagoId;
                contrato.TipoMonedaId = request.TipoMonedaId;
                contrato.GrupoNominaId = request.GrupoNominaId;
                contrato.EntidadFinancieraId = request.EntidadFinancieraId;
                contrato.TipoCuentaId = request.TipoCuentaId;
                contrato.NumeroCuenta = request.NumeroCuenta;
                contrato.RecibeDotacion = request.RecibeDotacion;
                contrato.JornadaLaboralId = request.JornadaLaboralId;
                contrato.EmpleadoConfianza = request.EmpleadoConfianza;
                contrato.ProcedimientoRetencion = (ProcedimientoRetenciones)request.ProcedimientoRetencion;
                contrato.Observaciones = Texto.TipoOracion(request.Observaciones);
                contrato.TipoPeriodoId = (int)request.TipoPeriodoId;
                contrato.TipoCotizanteSubtipoCotizanteId = (int)request.TipoCotizanteSubtipoCotizanteId;
                contrato.ColombianoEnElExterior = (bool)request.ColombianoEnElExterior;
                contrato.ExtranjeroNoObligadoACotizarAPension = (bool)request.ExtranjeroNoObligadoACotizarAPension;


                contexto.Contratos.Update(contrato);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(contrato);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
