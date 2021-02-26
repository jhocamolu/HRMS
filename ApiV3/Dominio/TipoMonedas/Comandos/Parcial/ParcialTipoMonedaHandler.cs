using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoMonedas.Comandos.Parcial
{
    public class ParcialTipoMonedaHandler : IRequestHandler<ParcialTipoMonedaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ParcialTipoMonedaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialTipoMonedaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var tipoMoneda = contexto.TipoMonedas.Find(request.Id);
                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        tipoMoneda.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        tipoMoneda.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                if (request.Nombre != null)
                {
                    tipoMoneda.Nombre = Texto.TipoOracion(request.Nombre.ToUpper());
                }
                if (request.Codigo != null)
                {
                    tipoMoneda.Codigo = request.Codigo.ToUpper();
                }
                contexto.TipoMonedas.Update(tipoMoneda);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(tipoMoneda);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
