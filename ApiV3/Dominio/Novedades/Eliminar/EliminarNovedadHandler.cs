using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Novedades.Comandos.Eliminar
{
    public class EliminarNovedadHandler : IRequestHandler<EliminarNovedadRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarNovedadHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarNovedadRequest request, CancellationToken cancellationToken)
        {
            Novedad novedad = contexto.Novedades.Find(request.Id);
            try
            {
                contexto.Novedades.Remove(novedad);
                await contexto.SaveChangesAsync();

                var consulta = contexto.NovedadSubperiodos.Where(x => x.NovedadId == request.Id).ToList();

                contexto.NovedadSubperiodos.RemoveRange(consulta);
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
