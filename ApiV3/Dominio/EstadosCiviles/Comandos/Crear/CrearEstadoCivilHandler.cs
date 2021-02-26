using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.EstadosCiviles.Comandos.Crear
{
    public class CrearEstadoCivilHandler : IRequestHandler<CrearEstadoCivilRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearEstadoCivilHandler(NominaDbContext context)
        {
            this.contexto = context;
        }

        public async Task<CommandResult> Handle(CrearEstadoCivilRequest request, CancellationToken cancellationToken)
        {
            try
            {
                EstadoCivil estadoCivil = new EstadoCivil
                {
                    Nombre = Texto.TipoOracion(request.Nombre.ToUpper())
                };

                this.contexto.EstadoCiviles.Add(estadoCivil);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(estadoCivil);
            }
            catch (Exception)
            {

                return CommandResult.Fail("El estado civil  que intentas crear ya existe");
            }
        }
    }
}
