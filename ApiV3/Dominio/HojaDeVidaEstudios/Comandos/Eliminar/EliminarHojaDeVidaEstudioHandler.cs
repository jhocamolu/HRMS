using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.HojaDeVidaEstudios.Comandos.Eliminar
{
    public class EliminarHojaDeVidaEstudioHandler : IRequestHandler<EliminarHojaDeVidaEstudioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public EliminarHojaDeVidaEstudioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarHojaDeVidaEstudioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                HojaDeVidaEstudio estudio = await contexto.HojaDeVidaEstudios.FindAsync(request.Id);
                if (estudio == null) return CommandResult.Fail("No existe", 404);

                contexto.HojaDeVidaEstudios.Remove(estudio);
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