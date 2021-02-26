using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.DeduccionRetefuentes.Comandos.Eliminar
{
    public class EliminarDeduccionRetefuenteHandler : IRequestHandler<EliminarDeduccionRetefuenteRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarDeduccionRetefuenteHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarDeduccionRetefuenteRequest request, CancellationToken cancellationToken)
        {
            try
            {
                DeduccionRetefuente deduccionRetefuente = await this.contexto.DeduccionRetefuentes.FindAsync(request.Id);
                var hoy = DateTime.Today;

                if (deduccionRetefuente == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }
                else
                {
                    var vigencia = contexto.AnnoVigencias.FirstOrDefault(x => x.Id == deduccionRetefuente.AnnoVigenciaId);
                    if (vigencia.Estado != EstadoAnnoVigencia.Vigente)
                    {
                        return CommandResult.Fail(
                            "Solo puedes eliminar una deducción de retefuente para para el año en vigente.", 404);
                    }

                }
                this.contexto.DeduccionRetefuentes.Remove(deduccionRetefuente);
                await contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
