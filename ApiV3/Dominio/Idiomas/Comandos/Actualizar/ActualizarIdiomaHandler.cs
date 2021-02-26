using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Idiomas.Comandos.Actualizar
{
    public class ActualizarIdiomaHandler : IRequestHandler<ActualizarIdiomaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarIdiomaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarIdiomaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Idioma idioma = contexto.Idiomas.Find(request.Id);

                idioma.Codigo = request.Codigo.ToLower();
                idioma.Nombre = Texto.TipoOracion(request.Nombre);

                contexto.Idiomas.Update(idioma);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(idioma);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
