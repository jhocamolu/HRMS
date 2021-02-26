using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.DivisionPoliticaNiveles2.Comandos.Eliminar
{
    public class EliminarDivisionPoliticaNivel2Handler : IRequestHandler<EliminarDivisionPoliticaNivel2Request, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarDivisionPoliticaNivel2Handler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarDivisionPoliticaNivel2Request request, CancellationToken cancellationToken)
        {
            try
            {
                DivisionPoliticaNivel2 divisionPoliticaNivel2 = this.contexto.DivisionPoliticaNiveles2.Find(request.Id);

                contexto.DivisionPoliticaNiveles2.Remove(divisionPoliticaNivel2);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(divisionPoliticaNivel2);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
