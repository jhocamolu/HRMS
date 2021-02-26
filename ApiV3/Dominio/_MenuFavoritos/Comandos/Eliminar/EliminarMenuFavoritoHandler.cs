using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio._MenuFavoritos.Comandos.Eliminar
{
    public class EliminarMenuFavoritoHandler : IRequestHandler<EliminarMenuFavoritoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarMenuFavoritoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarMenuFavoritoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var menu = contexto._MenuFavoritos.FirstOrDefault(x => x.Id == request.Id);

                if (menu == null)
                {
                    return CommandResult.Fail("El item del menu que intentas eliminar no existe.", 400);
                }
                else
                {
                    await contexto.Database.ExecuteSqlRawAsync("DELETE FROM util._MenuFavorito WHERE Id = {0}", request.Id);
                }
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
