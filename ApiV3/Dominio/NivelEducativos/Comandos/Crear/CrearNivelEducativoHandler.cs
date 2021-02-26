using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de crear registros en NivelEducativo
/// </summary>

namespace ApiV3.Dominio.NivelEducativos.Comandos.Crear
{
    public class CrearNivelEducativoHandler : IRequestHandler<CrearNivelEducativoRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;
        public CrearNivelEducativoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearNivelEducativoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                NivelEducativo nivelEducativo = new NivelEducativo
                {
                    Nombre = Texto.TipoOracion(request.Nombre),
                    Orden = (int)request.Orden
                };

                this.contexto.NivelEducativos.Add(nivelEducativo);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(nivelEducativo);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
