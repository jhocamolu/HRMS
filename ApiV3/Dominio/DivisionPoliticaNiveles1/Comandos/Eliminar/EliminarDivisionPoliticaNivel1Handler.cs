using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.DivisionPoliticaNiveles1.Comandos.Eliminar
{
    public class EliminarDivisionPoliticaNivel1Handler : IRequestHandler<EliminarDivisionPoliticaNivel1Request, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarDivisionPoliticaNivel1Handler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarDivisionPoliticaNivel1Request request, CancellationToken cancellationToken)
        {
            try
            {
                DivisionPoliticaNivel1 divisionPoliticaNivel1 = this.contexto.DivisionPoliticaNiveles1.Find(request.Id);

                contexto.DivisionPoliticaNiveles1.Remove(divisionPoliticaNivel1);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(divisionPoliticaNivel1);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
