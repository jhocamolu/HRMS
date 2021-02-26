using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.EstadosCiviles.Comandos.Estado
{
    public class EstadoEstadoCivilHandler : IRequestHandler<EstadoEstadoCivilRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EstadoEstadoCivilHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(EstadoEstadoCivilRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var estadoCivil = contexto.EstadoCiviles.Find(request.Id);
                if (request.Activo)
                {
                    estadoCivil.EstadoRegistro = EstadoRegistro.Activo;
                }
                else
                {
                    estadoCivil.EstadoRegistro = EstadoRegistro.Inactivo;
                }
                contexto.EstadoCiviles.Update(estadoCivil);
                await contexto.SaveChangesAsync();
                return CommandResult.Success(estadoCivil);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);

            }


        }
    }
}
