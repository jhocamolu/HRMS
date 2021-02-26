using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.DivisionPoliticaNiveles2.Comandos.Actualizar
{
    public class ActualizarDivisionPoliticaNivel2Handler : IRequestHandler<ActualizarDivisionPoliticaNivel2Request, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarDivisionPoliticaNivel2Handler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarDivisionPoliticaNivel2Request request, CancellationToken cancellationToken)
        {
            try
            {
                var divisionPoliticaNivel1 = contexto.DivisionPoliticaNiveles1.FirstOrDefault(x => x.Id == request.DivisionPoliticaNivel1Id);

                DivisionPoliticaNivel2 divisionPoliticaNivel2 = this.contexto.DivisionPoliticaNiveles2.Find(request.Id);

                divisionPoliticaNivel2.Codigo = divisionPoliticaNivel1.Codigo + request.Codigo;
                divisionPoliticaNivel2.Nombre = Texto.TipoOracion(request.Nombre);
                divisionPoliticaNivel2.DivisionPoliticaNivel1Id = request.DivisionPoliticaNivel1Id;

                contexto.DivisionPoliticaNiveles2.Update(divisionPoliticaNivel2);
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
