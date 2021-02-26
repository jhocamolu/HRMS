using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CuantaBancaria.Comandos.Eliminar
{
    public class EliminarCuentaBancariaHandler : IRequestHandler<EliminarCuentaBancariaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarCuentaBancariaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarCuentaBancariaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                CuentaBancaria cuenta = await this.contexto.CuentaBancarias.FindAsync(request.Id);
                if (cuenta == null)
                {
                    CommandResult.Fail("No existe", 404);
                }
                this.contexto.CuentaBancarias.Remove(cuenta);
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
