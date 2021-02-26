using ApiV3.Dominio.AplicacionExternaCargos.Comandos.Actualizar;
using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.AplicacionExternas.Comandos.Actualizar
{
    public class ActualizarAplicacionExternaCargoHandler : IRequestHandler<ActualizarAplicacionExternaCargoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarAplicacionExternaCargoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarAplicacionExternaCargoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                AplicacionExternaCargo aplicacionExternaCargo = this.contexto.AplicacionExternaCargos.Find(request.Id);

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

                contexto.AplicacionExternaCargos.Update(aplicacionExternaCargo);
                await contexto.SaveChangesAsync();

                if (request.CargoDependencia.Count >= 1)
                {
                    //Eliminamos los subperiodos para la libranza
                    var cargoDependienteBorrar = this.contexto.AplicacionExternaCargoDependientes.Where(x => x.AplicacionExternaCargoId == aplicacionExternaCargo.Id)
                                                                                            .ToList();
                    foreach (var item in cargoDependienteBorrar)
                    {
                        this.contexto.AplicacionExternaCargoDependientes.Remove(item);
                        await this.contexto.SaveChangesAsync();
                    }

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
