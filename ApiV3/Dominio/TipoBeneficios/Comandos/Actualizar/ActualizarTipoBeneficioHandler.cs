using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoBeneficios.Comandos.Actualizar
{
    public class ActualizarTipoBeneficioHandler : IRequestHandler<ActualizarTipoBeneficioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarTipoBeneficioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarTipoBeneficioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoBeneficio tipoBeneficio = contexto.TipoBeneficios.FirstOrDefault(x => x.Id == request.Id);

                tipoBeneficio.Nombre = Texto.TipoOracion(request.Nombre);
                tipoBeneficio.ConceptoNominaDevengoId = request.ConceptoNominaDevengoId;
                tipoBeneficio.ConceptoNominaDeduccionId = request.ConceptoNominaDeduccionId;
                tipoBeneficio.ConceptoNominaCalculoId = request.ConceptoNominaCalculoId;
                tipoBeneficio.RequiereAprobacionJefe = (bool)request.RequiereAprobacionJefe;
                tipoBeneficio.MontoMaximo = (double)request.MontoMaximo;
                tipoBeneficio.ValorSolicitado = (bool)request.ValorSolicitado;
                tipoBeneficio.PlazoMes = (bool)request.PlazoMes;
                tipoBeneficio.CuotaPermitida = (int)request.CuotaPermitida;
                tipoBeneficio.PeriodoPago = (bool)request.PeriodoPago;
                tipoBeneficio.PermisoEstudio = (bool)request.PermisoEstudio;
                tipoBeneficio.PermiteAuxilioEducativo = (bool)request.PermiteAuxilioEducativo;
                tipoBeneficio.PermiteDescuentoNomina = (bool)request.PermiteDescuentoNomina;
                tipoBeneficio.ValorAutorizado = (bool)request.ValorAutorizado;
                tipoBeneficio.DiasAntiguedad = (int)request.DiasAntiguedad;
                tipoBeneficio.VecesAnio = (int)request.VecesAnio;
                tipoBeneficio.Descripcion = request.Descripcion;

                this.contexto.TipoBeneficios.Update(tipoBeneficio);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(tipoBeneficio);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
