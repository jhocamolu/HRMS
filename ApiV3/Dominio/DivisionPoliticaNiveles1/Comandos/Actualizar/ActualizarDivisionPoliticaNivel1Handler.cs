using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.DivisionPoliticaNiveles1.Comandos.Actualizar
{
    public class ActualizarDivisionPoliticaNivel1Handler : IRequestHandler<ActualizarDivisionPoliticaNivel1Request, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarDivisionPoliticaNivel1Handler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarDivisionPoliticaNivel1Request request, CancellationToken cancellationToken)
        {
            try
            {
                DivisionPoliticaNivel1 divisionPoliticaNivel1 = this.contexto.DivisionPoliticaNiveles1.Find(request.Id);

                divisionPoliticaNivel1.Codigo = request.Codigo;
                divisionPoliticaNivel1.Nombre = Texto.TipoOracion(request.Nombre);
                divisionPoliticaNivel1.PaisId = request.PaisId;

                contexto.DivisionPoliticaNiveles1.Update(divisionPoliticaNivel1);
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
