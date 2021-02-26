using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Idiomas.Comandos.Crear
{
    public class CrearIdiomaHandler : IRequestHandler<CrearIdiomaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearIdiomaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(CrearIdiomaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Idioma idioma = new Idioma
                {
                    Codigo = request.Codigo.ToLower(),
                    Nombre = Texto.TipoOracion(request.Nombre)
                };
                contexto.Idiomas.Add(idioma);
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
