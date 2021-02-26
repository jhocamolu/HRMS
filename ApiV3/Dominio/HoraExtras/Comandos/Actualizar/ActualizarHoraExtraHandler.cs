using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.HoraExtras.Comandos.Actualizar
{
    public class ActualizarHoraExtraHandler : IRequestHandler<ActualizarHoraExtraRequest, CommandResult>
    {
        private NominaDbContext contexto;

        public ActualizarHoraExtraHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarHoraExtraRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var cantidad = request.Cantidad.ToString();
                cantidad = cantidad.Replace(",", ".");
                HoraExtra horaExtra = contexto.HoraExtras.Find(request.Id);
                horaExtra.FuncionarioId = (int)request.FuncionarioId;
                horaExtra.Fecha = (DateTime)request.Fecha;
                horaExtra.TipoHoraExtraId = (int)request.TipoHoraExtraId;
                horaExtra.Cantidad = cantidad;

                contexto.HoraExtras.Update(horaExtra);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(horaExtra);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
