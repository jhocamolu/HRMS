using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.BeneficioAdjuntos.Comandos.Parcial
{
    public class ParcialBeneficioAdjuntosHandler : IRequestHandler<ParcialBeneficioAdjuntosRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialBeneficioAdjuntosHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialBeneficioAdjuntosRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var beneficioAdjunto = await this.contexto.BeneficioAdjuntos.FindAsync(request.Id);
                if (request.BeneficioId != null) beneficioAdjunto.BeneficioId = (int)request.BeneficioId;
                if (request.TipoBeneficioRequisitoId != null) beneficioAdjunto.TipoBeneficioRequisitoId = (int)request.TipoBeneficioRequisitoId;
                if (request.Adjunto != null) beneficioAdjunto.Adjunto = request.Adjunto;

                if (request.Activo != null)
                {
                    beneficioAdjunto.EstadoRegistro = EstadoRegistro.Activo;
                    if (request.Activo == false)
                    {
                        beneficioAdjunto.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }

                this.contexto.BeneficioAdjuntos.Update(beneficioAdjunto);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(beneficioAdjunto);
            }
            catch (System.Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
