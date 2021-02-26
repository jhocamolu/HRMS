using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Calendarios.Comandos.Crear
{
    public class CrearCalendarioHandler : IRequestHandler<CrearCalendarioRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearCalendarioHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearCalendarioRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Calendario calendario = new Calendario
                {
                    Fecha = request.Fecha,
                    Nombre = Texto.TipoOracion(request.Nombre.ToLower())
                };

                contexto.Calendarios.Add(calendario);
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