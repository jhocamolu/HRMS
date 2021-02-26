using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Calendarios.Comandos.Actualizar
{
    public class ActualizarCalendarioHandler : IRequestHandler<ActualizarCalendarioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarCalendarioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarCalendarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Calendario calendario = this.contexto.Calendarios.FirstOrDefault(x => x.Id == request.Id);
                calendario.Fecha = request.Fecha;
                calendario.Nombre = Texto.TipoOracion(request.Nombre.ToLower());
                contexto.Calendarios.Update(calendario);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(calendario);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}