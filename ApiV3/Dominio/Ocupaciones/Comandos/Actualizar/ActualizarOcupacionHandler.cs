using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Ocupaciones.Comandos.Actualizar
{
    public class ActualizarOcupacionHandler : IRequestHandler<ActualizarOcupacionRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarOcupacionHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(ActualizarOcupacionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Ocupacion ocupacion = contexto.Ocupaciones.Find(request.Id);

                ocupacion.Codigo = request.Codigo;
                ocupacion.Nombre = Texto.TipoOracion(request.Nombre.ToUpper());

                contexto.Ocupaciones.Update(ocupacion);
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
