using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Clase encargada de crear lo0s registros en la entidad Profesiones
/// </summary>

namespace ApiV3.Dominio.Profesiones.Comandos.Crear
{
    public class CrearProfesionHandler : IRequestHandler<CrearProfesionRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearProfesionHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(CrearProfesionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Profesion profesion = new Profesion
                {
                    Nombre = Texto.TipoOracion(request.Nombre)
                };

                this.contexto.Profesiones.Add(profesion);
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