using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.HojaDeVidas.Comandos.Eliminar
{
    public class EliminarHojaDeVidaHandler : IRequestHandler<EliminarHojaDeVidaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public EliminarHojaDeVidaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarHojaDeVidaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                HojaDeVida InformacionHojaDeVida = await contexto.HojaDeVidas.FindAsync(request.Id);
                if (InformacionHojaDeVida == null) return CommandResult.Fail("No existe", 404);

                contexto.HojaDeVidas.Remove(InformacionHojaDeVida);
                await contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
