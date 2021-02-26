using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Administradoras.Comandos.Actualizar
{
    public class ActualizarAdministradoraHandler : IRequestHandler<ActualizarAdministradoraRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarAdministradoraHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarAdministradoraRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Administradora administradora = this.contexto.Administradoras.Find(request.Id);

                administradora.Codigo = request.Codigo;
                administradora.Nit = request.Nit;
                administradora.Dv = request.Dv;
                administradora.Nombre = request.Nombre.ToUpper();
                administradora.TipoAdministradoraId = (int)request.TipoAdministradoraId;

                contexto.Administradoras.Update(administradora);
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
