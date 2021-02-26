using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de realizar las actualizaciones generales a
/// la entidad Profesiones.
/// </summary>

namespace ApiV3.Dominio.Profesiones.Comandos.Actualizar
{
    public class ActualizarProfesionHandler : IRequestHandler<ActualizarProfesionRequest, CommandResult>
    {

        private readonly NominaDbContext contexto;
        public ActualizarProfesionHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }


        public async Task<CommandResult> Handle(ActualizarProfesionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Profesion profesion = this.contexto.Profesiones.Find(request.Id);

                profesion.Nombre = Texto.TipoOracion(request.Nombre);

                this.contexto.Profesiones.Update(profesion);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(profesion);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}