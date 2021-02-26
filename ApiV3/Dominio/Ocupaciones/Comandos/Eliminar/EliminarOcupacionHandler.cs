using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Ocupaciones.Comandos.Eliminar
{
    public class EliminarOcupacionHandler : IRequestHandler<EliminarOcupacionRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarOcupacionHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarOcupacionRequest request, CancellationToken cancellationToken)
        {
            Ocupacion ocupacion = contexto.Ocupaciones.Find(request.Id);
            try
            {
                contexto.Ocupaciones.Remove(ocupacion);
                await contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
