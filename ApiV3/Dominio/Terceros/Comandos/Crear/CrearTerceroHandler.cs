using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Terceros.Comandos.Crear
{
    public class CrearTerceroHandler : IRequestHandler<CrearTerceroRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearTerceroHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearTerceroRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Tercero tercero = new Tercero
                {
                    Nombre = request.Nombre,
                    Nit = request.Nit,
                    DigitoVerificacion = (int)request.DigitoVerificacion,
                    DivisionPoliticaNivel2Id = (int)request.DivisionPoliticaNivel2Id,
                    Telefono = request.Telefono,
                    Direccion = request.Direccion,
                    EntidadFinancieraId = (int)request.EntidadFinancieraId,
                    TipoCuentaId = (int)request.TipoCuentaId,
                    NumeroCuenta = request.NumeroCuenta,
                    Descripcion = request.Descripcion,
                };

                contexto.Terceros.Add(tercero);
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
