using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.DivisionPoliticaNiveles1.Comandos.Parcial
{
    public class ParcialDivisionPoliticaNivel1Handler : IRequestHandler<ParcialDivisionPoliticaNivel1Request, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialDivisionPoliticaNivel1Handler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialDivisionPoliticaNivel1Request request, CancellationToken cancellationToken)
        {
            try
            {
                DivisionPoliticaNivel1 divisionPoliticaNivel1 = this.contexto.DivisionPoliticaNiveles1.Find(request.Id);

                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        divisionPoliticaNivel1.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        divisionPoliticaNivel1.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }

                if (request.Codigo != null)
                {
                    divisionPoliticaNivel1.Codigo = request.Codigo;
                }
                if (request.Nombre != null)
                {
                    divisionPoliticaNivel1.Nombre = Texto.TipoOracion(request.Nombre);
                }

                if (request.PaisId != 0)
                {
                    divisionPoliticaNivel1.PaisId = request.PaisId;
                }


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
