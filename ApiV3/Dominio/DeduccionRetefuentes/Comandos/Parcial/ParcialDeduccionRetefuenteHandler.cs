using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.DeduccionRetefuentes.Comandos.Parcial
{
    public class ParcialDeduccionRetefuenteHandler : IRequestHandler<ParcialDeduccionRetefuenteRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialDeduccionRetefuenteHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialDeduccionRetefuenteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                DeduccionRetefuente deduccionRetefuente = await this.contexto.DeduccionRetefuentes.FindAsync(request.Id);

                if (request.Activo != null)
                {
                    if (request.Activo == true)
                    {
                        deduccionRetefuente.EstadoRegistro = EstadoRegistro.Activo;
                    }
                    else
                    {
                        deduccionRetefuente.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                this.contexto.DeduccionRetefuentes.Update(deduccionRetefuente);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(deduccionRetefuente);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
