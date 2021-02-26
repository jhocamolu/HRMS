using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Terceros.Comandos.Actualizar
{
    public class ActualizarTerceroHandler : IRequestHandler<ActualizarTerceroRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarTerceroHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(ActualizarTerceroRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Tercero tercero = await contexto.Terceros.FindAsync(request.Id);

                tercero.Nombre = request.Nombre;
                tercero.Nit = request.Nit;
                tercero.DigitoVerificacion = (int)request.DigitoVerificacion;
                tercero.DivisionPoliticaNivel2Id = (int)request.DivisionPoliticaNivel2Id;
                tercero.Telefono = request.Telefono;
                tercero.Direccion = request.Direccion;
                tercero.EntidadFinancieraId = (int)request.EntidadFinancieraId;
                tercero.TipoCuentaId = (int)request.TipoCuentaId;
                tercero.NumeroCuenta = request.NumeroCuenta;
                tercero.Descripcion = request.Descripcion;


                contexto.Terceros.Update(tercero);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(tercero);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
