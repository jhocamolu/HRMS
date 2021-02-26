using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de elimiar registros en el modelo JornadaLaborales
/// </summary>

namespace ApiV3.Dominio.JornadaLaborales.Comandos.Eliminar
{
    public class EliminarJornadaLaboralHandler : IRequestHandler<EliminarJornadaLaboralRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public EliminarJornadaLaboralHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarJornadaLaboralRequest request, CancellationToken cancellationToken)
        {
            try
            {
                JornadaLaboral jornadaLaboral = await this.contexto.JornadaLaborales.FindAsync(request.Id);
                if (jornadaLaboral == null) return CommandResult.Fail("No existe", 404);


                this.contexto.JornadaLaborales.Remove(jornadaLaboral);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
