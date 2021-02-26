using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.AplicacionExternaCargos.Comandos.Crear
{
    public class CrearAplicacionExternaCargoHandler : IRequestHandler<CrearAplicacionExternaCargoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearAplicacionExternaCargoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(CrearAplicacionExternaCargoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                AplicacionExternaCargo aplicacionExternaCargo = new AplicacionExternaCargo();
                aplicacionExternaCargo.AplicacionExternaId = (int)request.AplicacionExternaId;
                aplicacionExternaCargo.Tipo = (TipoAplicacionExternaCargo)request.Tipo;

                if (request.CentroOperativoDependienteId != null)
                {
                    aplicacionExternaCargo.CentroOperativoDependienteId = (int)request.CentroOperativoDependienteId;
                }
                aplicacionExternaCargo.CargoDependenciaIndependienteId = (int)request.CargoDependenciaIndependienteId;
                if (request.CentroOperativoIndependienteId != null)
                {
                    aplicacionExternaCargo.CentroOperativoIndependienteId = (int)request.CentroOperativoIndependienteId;
                }

                contexto.AplicacionExternaCargos.Add(aplicacionExternaCargo);
                await contexto.SaveChangesAsync();

                if (request.CargoDependencia.Count >= 1)
                {
                    foreach (var item in request.CargoDependencia)
                    {
                        AplicacionExternaCargoDependiente aplicacionExternaCargoDependiente = new AplicacionExternaCargoDependiente();
                        aplicacionExternaCargoDependiente.CargoDependenciaId = (int)item.CargoDependenciaId;
                        aplicacionExternaCargoDependiente.AplicacionExternaCargoId = aplicacionExternaCargo.Id;
                        contexto.AplicacionExternaCargoDependientes.Add(aplicacionExternaCargoDependiente);
                        await contexto.SaveChangesAsync();
                    }
                }

                return CommandResult.Success(aplicacionExternaCargo);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
