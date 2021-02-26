using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.JornadaLaboralDias.Comandos.Parcial
{
    public class ParcialJornadaLaboralDiaHandler : IRequestHandler<ParcialJornadaLaboralDiaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ParcialJornadaLaboralDiaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialJornadaLaboralDiaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var jornadaLaboralDia = this.contexto.JornadaLaboralDias.Find(request.Id);

                if (request.Dia != null) jornadaLaboralDia.Dia = (DiaSemana)request.Dia;
                if (request.JornadaLaboralId != null) jornadaLaboralDia.JornadaLaboralId = (int)request.JornadaLaboralId;
                if (request.HoraInicioJornada != null) jornadaLaboralDia.HoraInicioJornada = (TimeSpan)request.HoraInicioJornada;
                if (request.HoraInicioDescanso != null) jornadaLaboralDia.HoraInicioDescanso = request.HoraInicioDescanso;
                if (request.HoraFinDescanso != null) jornadaLaboralDia.HoraFinDescanso = request.HoraFinDescanso;
                if (request.HoraFinJornada != null) jornadaLaboralDia.HoraFinJornada = (TimeSpan)request.HoraFinJornada;


                this.contexto.JornadaLaboralDias.Update(jornadaLaboralDia);
                await this.contexto.SaveChangesAsync();

                return CommandResult.Success(jornadaLaboralDia);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
