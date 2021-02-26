using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.JornadaLaboralDias.Comandos.Actualizar
{
    public class ActualizarJornadaLaboralDiaHandler : IRequestHandler<ActualizarJornadaLaboralDiaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ActualizarJornadaLaboralDiaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarJornadaLaboralDiaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                JornadaLaboralDia jornadaLaboralDia = this.contexto.JornadaLaboralDias.Find(request.Id);

                jornadaLaboralDia.Dia = request.Dia;
                jornadaLaboralDia.JornadaLaboralId = request.JornadaLaboralId;
                jornadaLaboralDia.HoraInicioJornada = request.HoraInicioJornada;
                jornadaLaboralDia.HoraInicioDescanso = request.HoraInicioDescanso;
                jornadaLaboralDia.HoraFinDescanso = request.HoraFinDescanso;
                jornadaLaboralDia.HoraFinJornada = request.HoraFinJornada;

                this.contexto.JornadaLaboralDias.Update(jornadaLaboralDia);
                await this.contexto.SaveChangesAsync();

                if (jornadaLaboralDia.JornadaLaboral.JornadaLaboralDias != null)
                {
                    jornadaLaboralDia.JornadaLaboral.JornadaLaboralDias = null;
                }
                return CommandResult.Success(jornadaLaboralDia);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
