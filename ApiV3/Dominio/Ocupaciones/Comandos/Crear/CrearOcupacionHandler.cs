using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Ocupaciones.Comandos.Crear
{
    public class CrearOcupacionHandler : IRequestHandler<CrearOcupacionRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearOcupacionHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(CrearOcupacionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Ocupacion ocupacion = new Ocupacion
                {
                    Codigo = request.Codigo,
                    Nombre = Texto.TipoOracion(request.Nombre.ToUpper())
                };

                contexto.Ocupaciones.Add(ocupacion);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(ocupacion);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
