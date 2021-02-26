using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Administradoras.Comandos.Crear
{
    public class CrearAdministradoraHandler : IRequestHandler<CrearAdministradoraRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearAdministradoraHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearAdministradoraRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Administradora administradora = new Administradora
                {
                    Codigo = request.Codigo,
                    Nit = request.Nit,
                    Dv = request.Dv,
                    Nombre = request.Nombre.ToUpper(),
                    TipoAdministradoraId = (int)request.TipoAdministradoraId
                };
                contexto.Administradoras.Add(administradora);

                await contexto.SaveChangesAsync();
                return CommandResult.Success(administradora);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }

        }
    }
}
