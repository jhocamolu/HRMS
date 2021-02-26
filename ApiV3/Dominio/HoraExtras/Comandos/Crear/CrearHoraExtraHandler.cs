using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.HoraExtras.Comandos.Crear
{
    public class CrearHoraExtraHandler : IRequestHandler<CrearHoraExtraRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearHoraExtraHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearHoraExtraRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var cantidad = request.Cantidad.ToString();
                cantidad = cantidad.Replace(",",".");
                HoraExtra horaExtra = new HoraExtra();
                horaExtra.FuncionarioId = (int)request.FuncionarioId;
                horaExtra.Fecha = (DateTime)request.Fecha;
                horaExtra.TipoHoraExtraId = (int)request.TipoHoraExtraId;
                horaExtra.Cantidad = cantidad;
                horaExtra.Estado = EstadoHoraExtra.Pendiente;
                horaExtra.FormaRegistro = FormaRegistroHoraExtra.Manual;

                contexto.HoraExtras.Add(horaExtra);
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
