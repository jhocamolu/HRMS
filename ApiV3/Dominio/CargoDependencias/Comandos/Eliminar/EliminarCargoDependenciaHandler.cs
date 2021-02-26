using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CargoDependencias.Comandos.Eliminar
{
    public class EliminarCargoDependenciaHandler : IRequestHandler<EliminarCargoDependenciaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarCargoDependenciaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarCargoDependenciaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                //Valida si el cargo esta asociado a un contrato o un otro si
                var validaCargoContrato = contexto.Contratos.FirstOrDefault(x => x.CargoDependenciaId == request.Id);
                if (validaCargoContrato == null)
                {
                    var validaCargoOtroSi = contexto.ContratoOtroSis.FirstOrDefault(x => x.CargoDependenciaId == request.Id);
                    if (validaCargoOtroSi == null)
                    {
                        CargoDependencia cargoDependencia = await this.contexto.CargoDependencias.FindAsync(request.Id);
                        if (cargoDependencia == null)
                        {
                            return CommandResult.Fail("No existe", 404);
                        }
                        this.contexto.CargoDependencias.Remove(cargoDependencia);
                        await contexto.SaveChangesAsync();

                        var listaCargoReporta = this.contexto.CargoReportas.Where(x => x.CargoDependenciaId == request.Id).ToList();
                        foreach (var item in listaCargoReporta)
                        {

                            this.contexto.CargoReportas.Remove(item);
                            await contexto.SaveChangesAsync();

                        }

                        return CommandResult.Success();
                    }
                    else
                    {
                        return CommandResult.Fail("No es posible eliminar este registro debido a que se encuentra relacionado con más información en el sistema.", 404);
                    }
                }
                else
                {
                    return CommandResult.Fail("No es posible eliminar este registro debido a que se encuentra relacionado con más información en el sistema.", 404);
                }
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
