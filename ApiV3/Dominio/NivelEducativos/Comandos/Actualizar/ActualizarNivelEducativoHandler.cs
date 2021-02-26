using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de actualizar registor en la entidad NivelEducativo
/// </summary>

namespace ApiV3.Dominio.NivelEducativos.Comandos.Actualizar
{
    public class ActualizarNivelEducativoHandler : IRequestHandler<ActualizarNivelEducativoRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;
        public ActualizarNivelEducativoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarNivelEducativoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                NivelEducativo nivelEducativo = this.contexto.NivelEducativos.Find(request.Id);

                nivelEducativo.Nombre = Texto.TipoOracion(request.Nombre);
                nivelEducativo.Orden = (int)request.Orden;

                this.contexto.NivelEducativos.Update(nivelEducativo);
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
