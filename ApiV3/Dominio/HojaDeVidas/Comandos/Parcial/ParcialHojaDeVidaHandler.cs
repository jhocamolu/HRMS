using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.HojaDeVidas.Comandos.Parcial
{
    public class ParcialHojaDeVidaHandler : IRequestHandler<ParcialHojaDeVidaRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;
        public ParcialHojaDeVidaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialHojaDeVidaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var hojaDeVida = contexto.HojaDeVidas.Find(request.Id);
                if (request.Adjunto != null)
                {
                    hojaDeVida.Adjunto = request.Adjunto;
                }

                if (request.Activo != null)
                {
                    hojaDeVida.EstadoRegistro = EstadoRegistro.Activo;
                    if (request.Activo != true)
                    {
                        hojaDeVida.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }

                contexto.HojaDeVidas.Update(hojaDeVida);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(hojaDeVida);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
