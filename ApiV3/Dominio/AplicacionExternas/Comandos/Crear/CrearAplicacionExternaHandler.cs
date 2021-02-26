using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.AplicacionExternas.Comandos.Crear
{
    public class CrearAplicacionExternaHandler : IRequestHandler<CrearAplicacionExternaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearAplicacionExternaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearAplicacionExternaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                AplicacionExterna aplicacionExterna = new AplicacionExterna();
                aplicacionExterna.Codigo = request.Codigo.ToUpper();
                aplicacionExterna.Nombre = Texto.TipoOracion(request.Nombre);
                aplicacionExterna.Descripcion = request.Descripcion;
                aplicacionExterna.Aprueba = (TipoAplicacionExterna)request.Aprueba;
                aplicacionExterna.Autoriza = (TipoAplicacionExterna)request.Autoriza;
                aplicacionExterna.Revisa = (TipoAplicacionExterna)request.Revisa;

                contexto.AplicacionExternas.Add(aplicacionExterna);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(aplicacionExterna);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
