using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CuantaBancaria.Comandos.Crear
{
    public class CrearCuentaBancariaHandler : IRequestHandler<CrearCuentaBancariaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearCuentaBancariaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearCuentaBancariaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                CuentaBancaria cuenta = new CuentaBancaria
                {
                    EntidadFinancieraId = (int)request.EntidadFinancieraId,
                    TipoCuentaId = (int)request.TipoCuentaId,
                    Numero = request.Numero,
                    Nombre = request.Nombre
                };

                contexto.CuentaBancarias.Add(cuenta);
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
