using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoBeneficios.Comandos.Crear
{
    public class CrearTipoBeneficioHandler : IRequestHandler<CrearTipoBeneficioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearTipoBeneficioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearTipoBeneficioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoBeneficio tipoBeneficio = new TipoBeneficio
                {
                    Nombre = Texto.TipoOracion(request.Nombre),
                    ConceptoNominaDevengoId = request.ConceptoNominaDevengoId,
                    ConceptoNominaDeduccionId = request.ConceptoNominaDeduccionId,
                    ConceptoNominaCalculoId = request.ConceptoNominaCalculoId,
                    RequiereAprobacionJefe = (bool)request.RequiereAprobacionJefe,
                    MontoMaximo = (double)request.MontoMaximo,
                    ValorSolicitado = (bool)request.ValorSolicitado,
                    PlazoMes = (bool)request.PlazoMes,
                    CuotaPermitida = (int)request.CuotaPermitida,
                    PeriodoPago = (bool)request.PeriodoPago,
                    PermisoEstudio = (bool)request.PermisoEstudio,
                    PermiteAuxilioEducativo = (bool)request.PermiteAuxilioEducativo,
                    PermiteDescuentoNomina = (bool)request.PermiteDescuentoNomina,
                    ValorAutorizado = (bool)request.ValorAutorizado,
                    DiasAntiguedad = (int)request.DiasAntiguedad,
                    VecesAnio = (int)request.VecesAnio,
                    Descripcion = request.Descripcion
                };
                this.contexto.TipoBeneficios.Add(tipoBeneficio);
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
