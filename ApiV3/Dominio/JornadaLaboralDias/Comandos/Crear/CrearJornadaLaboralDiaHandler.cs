using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.JornadaLaboralDias.Comandos.Crear
{
    public class CrearJornadaLaboralDiaHandler : IRequestHandler<CrearJornadaLaboralDiaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public CrearJornadaLaboralDiaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(CrearJornadaLaboralDiaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                JornadaLaboralDia jornadaLaboralDia = new JornadaLaboralDia
                {
                    JornadaLaboralId = request.JornadaLaboralId,
                    Dia = request.Dia,
                    HoraInicioJornada = request.HoraInicioJornada,
                    HoraInicioDescanso = request.HoraInicioDescanso,
                    HoraFinDescanso = request.HoraFinDescanso,
                    HoraFinJornada = request.HoraFinJornada
                };
                this.contexto.JornadaLaboralDias.Add(jornadaLaboralDia);
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
