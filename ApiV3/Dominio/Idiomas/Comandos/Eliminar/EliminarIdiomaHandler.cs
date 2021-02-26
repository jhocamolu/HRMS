using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Idiomas.Comandos.Eliminar
{
    public class EliminarIdiomaHandler : IRequestHandler<EliminarIdiomaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarIdiomaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarIdiomaRequest request, CancellationToken cancellationToken)
        {
            Idioma idioma = contexto.Idiomas.Find(request.Id);
            try
            {
                contexto.Idiomas.Remove(idioma);
                await contexto.SaveChangesAsync();
            }
            catch (Exception e)

            {
                return CommandResult.Fail(e.Message);
            }
            return CommandResult.Success(idioma);

        }
    }
}
