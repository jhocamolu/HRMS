using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.EstadosCiviles.Comandos.Actualizar
{
    public class ActualizarEstadoCivilHandler : IRequestHandler<ActualizarEstadoCivilRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ActualizarEstadoCivilHandler(NominaDbContext context)
        {
            this.contexto = context;
        }

        public async Task<CommandResult> Handle(ActualizarEstadoCivilRequest request, CancellationToken cancellationToken)
        {
            try
            {
                EstadoCivil estadoCivil = this.contexto.EstadoCiviles.Find(request.Id);
                estadoCivil.Nombre = Texto.TipoOracion(request.Nombre.ToUpper());
                this.contexto.EstadoCiviles.Update(estadoCivil);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(estadoCivil);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
