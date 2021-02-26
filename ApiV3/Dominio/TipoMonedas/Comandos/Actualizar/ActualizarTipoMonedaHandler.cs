using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoMonedas.Comandos.Actualizar
{
    public class ActualizarTipoMonedaHandler : IRequestHandler<ActualizarTipoMonedaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarTipoMonedaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarTipoMonedaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoMoneda tipoMoneda = this.contexto.TipoMonedas.Find(request.Id);
                tipoMoneda.Nombre = Texto.TipoOracion(request.Nombre.ToUpper());
                tipoMoneda.Codigo = request.Codigo.ToUpper();
                this.contexto.TipoMonedas.Update(tipoMoneda);
                await this.contexto.SaveChangesAsync();

                return CommandResult.Success(tipoMoneda);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
