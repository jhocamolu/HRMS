using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.Embargos.Comandos.Parcial
{
    public class ParcialEmbargosHandler : IRequestHandler<ParcialEmbargosRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ParcialEmbargosHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialEmbargosRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Embargo embargo = await this.contexto.Embargos.FindAsync(request.Id);
                if (request.NumeroProceso != null) embargo.NumeroProceso = request.NumeroProceso;
                if (request.ValorEmbargo != null) embargo.ValorEmbargo = (double)request.ValorEmbargo;
                if (request.ValorCuota != null) embargo.ValorCuota = (double)request.ValorCuota;

                if (request.EntidadFinancieraId != null) embargo.EntidadFinancieraId = (int)request.EntidadFinancieraId;
                if (request.NumeroCuenta != null) embargo.NumeroCuenta = request.NumeroCuenta;
                if (request.NumeroDocumentoDemandante != null) embargo.NumeroDocumentoDemandante = Texto.TipoOracion(request.NumeroDocumentoDemandante);
                if (embargo.DigitoVerificacionDemandante != null) embargo.DigitoVerificacionDemandante = (int)request.DigitoVerificacionDemandante;
                if (request.Demandante != null) embargo.Demandante = request.Demandante;
                if (request.FechaFin != null) embargo.FechaFin = (DateTime)request.FechaFin;
                if (request.Estado != null) embargo.Estado = (EstadoEmbargo)request.Estado;
                if (request.PorcentajeCuota != null) embargo.PorcentajeCuota = request.PorcentajeCuota;

                if (request.Activo != null)
                {
                    embargo.EstadoRegistro = EstadoRegistro.Activo;
                    if (request.Activo == false)
                    {
                        embargo.EstadoRegistro = EstadoRegistro.Inactivo;
                    }
                }
                #region Prioridad
                // Consulta ultimo embargo creado
                if (request.Prioridad != null)
                {
                    var ultimoCreado = contexto.Embargos.Where(x => x.FuncionarioId == embargo.FuncionarioId &&
                                                                    x.Estado != EstadoEmbargo.Anulado &&
                                                                    x.Estado != EstadoEmbargo.Terminado
                                                                 )
                                                         .OrderByDescending(x => x.Prioridad)
                                                         .FirstOrDefault();
                    if (ultimoCreado != null)
                    {
                        if (ultimoCreado.Prioridad < request.Prioridad)
                        {
                            embargo.Prioridad = ultimoCreado.Prioridad + 1;
                        }
                        else
                        {
                            embargo.Prioridad = (int)request.Prioridad;
                        }
                    }
                    else
                    {
                        embargo.Prioridad = (int)request.Prioridad;
                    }
                }
                #endregion
                #region Estado
                if (embargo.Estado != EstadoEmbargo.Anulado &&
                    embargo.Estado != EstadoEmbargo.Terminado &&
                    embargo.Estado != EstadoEmbargo.Liquidado)
                {
                    if (request.FechaInicio != null)
                    {
                        embargo.FechaInicio = (DateTime)request.FechaInicio;

                        //Valida si la fecha de inicio es posterior a la fecha de inicio de la nómina
                        var validaNomina = contexto.Nominas.FirstOrDefault(x => x.EstadoRegistro == EstadoRegistro.Activo &&
                                                                                x.Estado == EstadoNomina.Inicializada);
                        if ((validaNomina.FechaInicio >= request.FechaInicio) && (request.FechaInicio <= validaNomina.FechaFinal))
                        {
                            embargo.Estado = EstadoEmbargo.Vigente;
                        }
                        else
                        {
                            embargo.Estado = EstadoEmbargo.Pendiente;
                        }
                        if (request.FechaInicio == DateTime.Now)
                        {
                            embargo.Estado = EstadoEmbargo.Pendiente;
                        }
                    }
                    else
                    {
                        embargo.Estado = EstadoEmbargo.Vigente;
                    }
                }
                #endregion  
                this.contexto.Embargos.Update(embargo);
                await this.contexto.SaveChangesAsync();
                #region Prioridad 
                if (embargo.ActualizaPrioridad == true)
                {
                    // Se actualizan todos los embargos que esten con una prioridad igual o superior a la prioridad 
                    // ingresada

                    var embargoCreado = contexto.Embargos.Where(x => x.FuncionarioId == embargo.FuncionarioId &&
                                                                x.Estado != EstadoEmbargo.Anulado &&
                                                                x.Estado != EstadoEmbargo.Terminado &&
                                                                x.Id != embargo.Id &&
                                                                x.Prioridad >= embargo.Prioridad
                                                                )
                                                     .Include(x => x.TipoEmbargo)
                                                     .OrderByDescending(x => x.Prioridad)
                                                     .ToList();

                    foreach (var item in embargoCreado)
                    {
                        //Actualiza registro con tipo embargo concepto nómina para la clase de concepto deducción.
                        Embargo actualizaPrioridadEmbargo = this.contexto.Embargos.Find(item.Id);
                        actualizaPrioridadEmbargo.Prioridad = actualizaPrioridadEmbargo.Prioridad + 1;
                        this.contexto.Embargos.Update(actualizaPrioridadEmbargo);
                    }
                }
                #endregion
                return CommandResult.Success(embargo);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}
