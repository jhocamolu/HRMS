using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CuantaBancaria.Comandos.Actualizar
{
    public class ActualizarCuentaBancariaHandler : IRequestHandler<ActualizarCuentaBancariaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarCuentaBancariaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarCuentaBancariaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                CuentaBancaria cuenta = await contexto.CuentaBancarias.FindAsync(request.Id);

                cuenta.EntidadFinancieraId = (int)request.EntidadFinancieraId;
                cuenta.TipoCuentaId = (int)request.TipoCuentaId;
                cuenta.Numero = request.Numero;
                cuenta.Nombre = request.Nombre;

                contexto.CuentaBancarias.Update(cuenta);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(cuenta);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
