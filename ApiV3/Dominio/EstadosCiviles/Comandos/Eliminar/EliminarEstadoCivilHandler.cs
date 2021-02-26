using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.EstadosCiviles.Comandos.Eliminar
{
    public class EliminarEstadoCivilHandler : IRequestHandler<EliminarEstadoCivilRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarEstadoCivilHandler(NominaDbContext context)
        {
            this.contexto = context;
        }


        public async Task<CommandResult> Handle(EliminarEstadoCivilRequest request, CancellationToken cancellationToken)
        {
            EstadoCivil estadoCivil = this.contexto.EstadoCiviles.Find(request.Id);
            try
            {
                this.contexto.EstadoCiviles.Remove(estadoCivil);
                await this.contexto.SaveChangesAsync();
            }
            catch (Exception e)

            {
                return CommandResult.Fail(e.Message);
            }
            return CommandResult.Success(estadoCivil);

        }
    }
}
