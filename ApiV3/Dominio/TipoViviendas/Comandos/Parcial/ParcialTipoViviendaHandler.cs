using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.TipoViviendas.Comandos.Estado
{
    public class ParcialTipoViviendaHandler : IRequestHandler<ParcialTipoViviendaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialTipoViviendaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(ParcialTipoViviendaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var tipoVivienda = contexto.TipoViviendas.Find(request.Id);
                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        tipoVivienda.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        tipoVivienda.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                if (request.Nombre != null)
                {
                    tipoVivienda.Nombre = Texto.TipoOracion(request.Nombre.ToLower());
                }

                contexto.TipoViviendas.Update(tipoVivienda);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(tipoVivienda);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
