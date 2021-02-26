using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Paises.Comandos.Actualizar
{
    public class ActualizarPaisHandler : IRequestHandler<ActualizarPaisRequest, CommandResult>
    {
        private readonly NominaDbContext context;

        public ActualizarPaisHandler(NominaDbContext context)
        {
            this.context = context;
        }

        public async Task<CommandResult> Handle(ActualizarPaisRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Pais pais = this.context.Paises.Find(request.Id);

                pais.Codigo = request.Codigo;
                pais.Nombre = request.Nombre;
                pais.Nacionalidad = request.Nacionalidad;

                this.context.Paises.Update(pais);
                await this.context.SaveChangesAsync();
                return CommandResult.Success(pais);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }

    }
}
