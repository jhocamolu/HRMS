using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CuantaBancaria.Comandos.Parcial
{
    public class ParcialCuentaBancariaHandler : IRequestHandler<ParcialCuentaBancariaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialCuentaBancariaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialCuentaBancariaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                CuentaBancaria cuenta = this.contexto.CuentaBancarias.Find(request.Id);
                if (request.Activo != null)
                {
                    cuenta.EstadoRegistro = EstadoRegistro.Activo;
                    if (request.Activo == false)
                    {
                        cuenta.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
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