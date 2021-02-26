using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de realizar las actualizaciones en el modelo JornadaLaborales
/// </summary>

namespace ApiV3.Dominio.JornadaLaborales.Comandos.Actualizar
{
    public class ActualizarJornadaLaboralHandler : IRequestHandler<ActualizarJornadaLaboralRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ActualizarJornadaLaboralHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarJornadaLaboralRequest request, CancellationToken cancellationToken)
        {
            try
            {
                JornadaLaboral jornadaLaboral = await this.contexto.JornadaLaborales.FindAsync(request.Id);
                jornadaLaboral.Nombre = Texto.TipoOracion(request.Nombre);

                this.contexto.JornadaLaborales.Update(jornadaLaboral);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(jornadaLaboral);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
