using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.CargoReportas.Eliminar
{
    public class EliminarCargoReportaHandler : IRequestHandler<EliminarCargoReportaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EliminarCargoReportaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EliminarCargoReportaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                CargoReporta cargoReporta = await this.contexto.CargoReportas.FindAsync(request.Id);
                if (cargoReporta.JefeInmediato == true)
                {
                    var dependencia = this.contexto.CargoDependencias.FirstOrDefault(x => x.Id == cargoReporta.CargoDependenciaReportaId);

                    //Consulta el siguiente en la lista para dejar cargo como jefe inmediato true.
                    var asignaJefeInmediato = this.contexto.CargoReportas.Where(x => x.Id != request.Id &&
                                                                        x.EstadoRegistro == EstadoRegistro.Activo &&
                                                                        x.CargoDependenciaId == cargoReporta.CargoDependenciaId)
                                                                        .OrderBy(x => x.FechaCreacion)
                                                                        .ToList();
                    if (asignaJefeInmediato != null)
                    {
                        foreach (var item in asignaJefeInmediato)
                        {
                            var dependenciaJefe = this.contexto.CargoDependencias.FirstOrDefault(x => x.Id == item.CargoDependenciaReportaId);

                            if (dependencia.DependenciaId == dependenciaJefe.DependenciaId)
                            {
                                //Actualiza el registro como jefe inmediato
                                item.JefeInmediato = true;
                                this.contexto.CargoReportas.Update(item);
                                await contexto.SaveChangesAsync();
                            }
                        }

                    }
                }
                if (cargoReporta == null)
                {
                    return CommandResult.Fail("No existe", 404);
                }

                this.contexto.CargoReportas.Remove(cargoReporta);
                await contexto.SaveChangesAsync();
                return CommandResult.Success();
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
