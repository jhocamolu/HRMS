using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Paises.Comandos.Crear
{
    public class CrearPaisHandler : IRequestHandler<CrearPaisRequest, CommandResult>
    {
        private readonly NominaDbContext context;

        public CrearPaisHandler(NominaDbContext context)
        {
            this.context = context;
        }

        public async Task<CommandResult> Handle(CrearPaisRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Pais pais = new Pais
                {
                    Codigo = request.Codigo,
                    Nombre = request.Nombre,
                    Nacionalidad = request.Nacionalidad
                };

                this.context.Paises.Add(pais);
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
