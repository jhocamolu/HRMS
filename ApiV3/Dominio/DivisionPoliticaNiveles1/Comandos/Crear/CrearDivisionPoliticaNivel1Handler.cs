using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.DivisionPoliticaNiveles1.Comandos.Crear
{
    public class CrearDivisionPoliticaNivel1Handler : IRequestHandler<CrearDivisionPoliticaNivel1Request, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearDivisionPoliticaNivel1Handler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearDivisionPoliticaNivel1Request request, CancellationToken cancellationToken)
        {
            try
            {
                DivisionPoliticaNivel1 divisionPoliticaNivel1 = new DivisionPoliticaNivel1
                {
                    Codigo = request.Codigo,
                    Nombre = Texto.TipoOracion(request.Nombre),
                    PaisId = request.PaisId
                };
                contexto.DivisionPoliticaNiveles1.Add(divisionPoliticaNivel1);

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
