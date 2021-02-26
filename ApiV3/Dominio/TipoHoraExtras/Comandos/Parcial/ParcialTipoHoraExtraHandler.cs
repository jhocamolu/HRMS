using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoHoraExtras.Comandos.Estado
{
    public class ParcialTipoHoraExtraHandler : IRequestHandler<ParcialTipoHoraExtraRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialTipoHoraExtraHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(ParcialTipoHoraExtraRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var tipoHoraExtra = contexto.TipoHoraExtras.Find(request.Id);
                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        tipoHoraExtra.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        tipoHoraExtra.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                contexto.TipoHoraExtras.Update(tipoHoraExtra);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(tipoHoraExtra);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
