using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoMonedas.Comandos.Crear
{
    public class CrearTipoMonedaHandler : IRequestHandler<CrearTipoMonedaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearTipoMonedaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearTipoMonedaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoMoneda tipoMoneda = new TipoMoneda
                {
                    Nombre = Texto.TipoOracion(request.Nombre.ToUpper()),
                    Codigo = request.Codigo.ToUpper()
                };
                this.contexto.TipoMonedas.Add(tipoMoneda);
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
