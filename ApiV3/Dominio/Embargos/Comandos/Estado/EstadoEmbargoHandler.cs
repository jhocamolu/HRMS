using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Embargos.Comandos.Estado
{
    public class EstadoEmbargoHandler : IRequestHandler<EstadoEmbargoRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public EstadoEmbargoHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(EstadoEmbargoRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Embargo embargo = contexto.Embargos.Find(request.Id);
                if (request.Estado != null)
                {
                    if (request.Estado == EstadoEmbargo.Anulado)
                    {
                        embargo.Estado = EstadoEmbargo.Anulado;
                    }
                    if (request.Estado == EstadoEmbargo.Terminado)
                    {
                        embargo.Estado = EstadoEmbargo.Terminado;
                    }
                }
                if (request.Justificacion != null)
                {
                    embargo.Justificacion = request.Justificacion;
                }
                contexto.Embargos.Update(embargo);
                await contexto.SaveChangesAsync();
                if (request.Estado == EstadoEmbargo.Anulado || request.Estado == EstadoEmbargo.Terminado)
                {
                    var embargoCreado = contexto.Embargos.Where(x => x.FuncionarioId == embargo.FuncionarioId &&
                                                                    x.Estado != EstadoEmbargo.Anulado &&
                                                                    x.Estado != EstadoEmbargo.Terminado &&
                                                                    x.Id != embargo.Id &&
                                                                    x.Prioridad > embargo.Prioridad
                                                                    )
                                                         .Include(x => x.TipoEmbargo)
                                                         .OrderByDescending(x => x.Prioridad)
                                                         .ToList();

                    //Actualiza la prioridad de los embargos.
                    foreach (var item in embargoCreado)
                    {
                        Embargo actualizaPrioridadEmbargo = this.contexto.Embargos.Find(item.Id);
                        actualizaPrioridadEmbargo.Prioridad = actualizaPrioridadEmbargo.Prioridad - 1;
                        this.contexto.Embargos.Update(actualizaPrioridadEmbargo);
                        await contexto.SaveChangesAsync();
                    }

                    //Actualizar estado de Pendiente a Vigente con menor prioridad
                    var actualizaEstadoVigente = contexto.Embargos.Where(x => x.FuncionarioId == embargo.FuncionarioId &&
                                                                                    x.Estado == EstadoEmbargo.Pendiente
                                                                                    )
                                                                  .OrderBy(x => x.Prioridad)
                                                                  .FirstOrDefault();
                    if (actualizaEstadoVigente != null)
                    {
                        actualizaEstadoVigente.Estado = EstadoEmbargo.Vigente;
                        this.contexto.Embargos.Update(actualizaEstadoVigente);
                        await contexto.SaveChangesAsync();
                    }
                }

                return CommandResult.Success(embargo);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
