using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CargoDependencias.Comandos.Crear
{
    public class CrearCargoDependenciaHandler : IRequestHandler<CrearCargoDependenciaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearCargoDependenciaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearCargoDependenciaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                CargoDependencia cargoDependencia = new CargoDependencia
                {
                    CargoId = request.CargoId,
                    DependenciaId = request.DependenciaId
                };

                this.contexto.CargoDependencias.Add(cargoDependencia);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(cargoDependencia);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }


        }
    }
}
