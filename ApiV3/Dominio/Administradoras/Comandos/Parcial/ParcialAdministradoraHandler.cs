using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Administradoras.Comandos.Estado
{
    public class ParcialAdministradoraHandler : IRequestHandler<ParcialAdministradoraRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialAdministradoraHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(ParcialAdministradoraRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var administradora = contexto.Administradoras.Find(request.Id);

                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        administradora.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        administradora.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                if (request.Codigo != null)
                {
                    administradora.Codigo = request.Codigo;
                }
                if (request.Nombre != null)
                {
                    administradora.Nombre = request.Nombre.ToUpper();
                }

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
