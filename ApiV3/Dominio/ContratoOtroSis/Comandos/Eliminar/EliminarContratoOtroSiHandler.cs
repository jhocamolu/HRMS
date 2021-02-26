using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ContratoOtroSis.Comandos.Eliminar
{
    public class EliminarContratoOtroSiHandler : IRequestHandler<EliminarContratoOtroSiRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public EliminarContratoOtroSiHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarContratoOtroSiRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ContratoOtroSi otroSi = await this.contexto.ContratoOtroSis.FindAsync(request.Id);

                int ultimoOtroSi = (from otro in contexto.ContratoOtroSis
                                    where otro.ContratoId == otroSi.ContratoId
                                    select otro.NumeroOtroSi).Max();

                if (ultimoOtroSi == otroSi.NumeroOtroSi)
                {
                    this.contexto.ContratoOtroSis.Remove(otroSi);
                    await this.contexto.SaveChangesAsync();
                    return CommandResult.Success(otroSi);
                }
                else
                {
                    return CommandResult.Fail("Sólo puede eliminar el último otrosí.", 400);
                }
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
