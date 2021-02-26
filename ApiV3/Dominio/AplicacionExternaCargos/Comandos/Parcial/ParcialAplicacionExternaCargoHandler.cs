using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.AplicacionExternaCargos.Comandos.Parcial
{
    public class ParcialAplicacionExternaCargoHandler : IRequestHandler<ParcialAplicacionExternaCargoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialAplicacionExternaCargoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialAplicacionExternaCargoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                AplicacionExternaCargo aplicacionExternaCargo = this.contexto.AplicacionExternaCargos.Find(request.Id);

                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        aplicacionExternaCargo.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    if (request.Activo == false)
                    {
                        aplicacionExternaCargo.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }

                contexto.AplicacionExternaCargos.Update(aplicacionExternaCargo);
                await contexto.SaveChangesAsync();

                return CommandResult.Success(aplicacionExternaCargo);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}