using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Terceros.Comandos.Parcial
{
    public class ParcialTerceroHandler : IRequestHandler<ParcialTerceroRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialTerceroHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(ParcialTerceroRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Tercero tercero = await contexto.Terceros.FindAsync(request.Id);

                tercero.EstadoRegistro = EstadoRegistro.Activo;
                if (request.Activo == false)
                {
                    tercero.EstadoRegistro = EstadoRegistro.Inactivo;
                }


                contexto.Terceros.Update(tercero);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(tercero);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
