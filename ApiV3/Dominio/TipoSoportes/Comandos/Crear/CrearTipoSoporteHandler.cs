using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoSoportes.Comandos.Crear
{
    public class CrearTipoSoporteHandler : IRequestHandler<CrearTipoSoporteRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearTipoSoporteHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearTipoSoporteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                TipoSoporte tipoSoporte = new TipoSoporte
                {
                    Nombre = Texto.TipoOracion(request.Nombre.ToLower())
                };
                this.contexto.TipoSoportes.Add(tipoSoporte);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(tipoSoporte);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
