using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.JornadaLaboralDias.Comandos.Eliminar
{
    public class EliminarJornadaLaboralDiaHandler : IRequestHandler<EliminarJornadaLaboralDiaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public EliminarJornadaLaboralDiaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarJornadaLaboralDiaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                JornadaLaboralDia jornadaLaboralDia = await this.contexto.JornadaLaboralDias.FindAsync(request.Id);
                if (jornadaLaboralDia == null) return CommandResult.Fail("No existe", 404);

                this.contexto.JornadaLaboralDias.Remove(jornadaLaboralDia);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
