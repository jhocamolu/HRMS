using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoSoportes.Comandos.Estado
{
    public class ParcialTipoSoporteHandler : IRequestHandler<ParcialTipoSoporteRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialTipoSoporteHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(ParcialTipoSoporteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var tipoSoporte = contexto.TipoSoportes.Find(request.Id);
                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        tipoSoporte.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        tipoSoporte.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                if (request.Nombre != null)
                {
                    tipoSoporte.Nombre = Texto.TipoOracion(request.Nombre.ToLower());
                }

                contexto.TipoSoportes.Update(tipoSoporte);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(tipoSoporte);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
