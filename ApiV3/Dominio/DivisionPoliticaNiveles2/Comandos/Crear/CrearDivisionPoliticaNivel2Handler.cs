using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.DivisionPoliticaNiveles2.Comandos.Crear
{
    public class CrearDivisionPoliticaNivel2Handler : IRequestHandler<CrearDivisionPoliticaNivel2Request, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearDivisionPoliticaNivel2Handler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearDivisionPoliticaNivel2Request request, CancellationToken cancellationToken)
        {
            try
            {
                var divisionPoliticaNivel1 = contexto.DivisionPoliticaNiveles1.FirstOrDefault(x => x.Id == request.DivisionPoliticaNivel1Id);

                DivisionPoliticaNivel2 divisionPoliticaNivel2 = new DivisionPoliticaNivel2
                {
                    Codigo = divisionPoliticaNivel1.Codigo + request.Codigo,
                    Nombre = Texto.TipoOracion(request.Nombre),
                    DivisionPoliticaNivel1Id = request.DivisionPoliticaNivel1Id
                };
                contexto.DivisionPoliticaNiveles2.Add(divisionPoliticaNivel2);

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
