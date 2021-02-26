using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.DivisionPoliticaNiveles2.Comandos.Parcial
{
    public class ParcialDivisionPoliticaNivel2Handler : IRequestHandler<ParcialDivisionPoliticaNivel2Request, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialDivisionPoliticaNivel2Handler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialDivisionPoliticaNivel2Request request, CancellationToken cancellationToken)
        {
            try
            {
                DivisionPoliticaNivel2 divisionPoliticaNivel2 = this.contexto.DivisionPoliticaNiveles2.Find(request.Id);

                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        divisionPoliticaNivel2.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        divisionPoliticaNivel2.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }

                if (request.Codigo != null)
                {
                    var divisionPoliticaNivel1 = contexto.DivisionPoliticaNiveles1.FirstOrDefault(x => x.Id == request.DivisionPoliticaNivel1Id);
                    divisionPoliticaNivel2.Codigo = divisionPoliticaNivel1.Codigo + request.Codigo;
                }
                if (request.Nombre != null)
                {
                    divisionPoliticaNivel2.Nombre = Texto.TipoOracion(request.Nombre);
                }

                if (request.DivisionPoliticaNivel1Id != 0)
                {
                    divisionPoliticaNivel2.DivisionPoliticaNivel1Id = request.DivisionPoliticaNivel1Id;
                }


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
