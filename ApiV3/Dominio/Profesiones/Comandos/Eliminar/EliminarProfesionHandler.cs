using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de eliminar profesiones    
/// </summary>

namespace ApiV3.Dominio.Profesiones.Comandos.Eliminar
{
    public class EliminarProfesionHandler : IRequestHandler<EliminarProfesionRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarProfesionHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(EliminarProfesionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Profesion profesion = await this.contexto.Profesiones.FindAsync(request.Id);
                if (profesion == null) return CommandResult.Fail("No existe", 404);

                this.contexto.Profesiones.Remove(profesion);
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
