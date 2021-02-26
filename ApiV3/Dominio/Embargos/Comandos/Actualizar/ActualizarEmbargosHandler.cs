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

namespace ApiV3.Dominio.Embargos.Comandos.Actualizar
{
    public class ActualizarEmbargosHandler : IRequestHandler<ActualizarEmbargosRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarEmbargosHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ActualizarEmbargosRequest request, CancellationToken cancellationToken)
        {
            try
            {
                Embargo embargo = await this.contexto.Embargos.FindAsync(request.Id);
                embargo.FuncionarioId = (int)request.FuncionarioId;
                embargo.JuzgadoId = request.JuzgadoId;
                embargo.NumeroProceso = request.NumeroProceso;
                embargo.ValorEmbargo = request.ValorEmbargo;
                embargo.ValorCuota = request.ValorCuota;
                embargo.PorcentajeCuota = request.PorcentajeCuota;

                #region Prioridad
                // Consulta ultimo embargo creado
                var ultimoCreado = contexto.Embargos.Where(x => x.FuncionarioId == request.FuncionarioId &&
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
                #endregion
                embargo.EntidadFinancieraId = (int)request.EntidadFinancieraId;
                embargo.NumeroCuenta = request.NumeroCuenta;
                embargo.NumeroDocumentoDemandante = request.NumeroDocumentoDemandante;
                embargo.DigitoVerificacionDemandante = request.DigitoVerificacionDemandante;
                embargo.Demandante = Texto.TipoOracion(request.Demandante);
                embargo.ActualizaPrioridad = request.ActualizaPrioridad;
                embargo.FechaFin = request.FechaFin;
                embargo.FechaInicio = request.FechaInicio;
                #region Estado
                if (embargo.Estado != EstadoEmbargo.Anulado &&
                     embargo.Estado != EstadoEmbargo.Terminado &&
                     embargo.Estado != EstadoEmbargo.Liquidado)
                {
                    if (request.FechaInicio != null)
                    {


                        //Valida si la fecha de inicio es posterior a la fecha de inicio de la nómina
                        var validaNomina = contexto.Nominas.FirstOrDefault(x => x.EstadoRegistro == EstadoRegistro.Activo &&
                                                                                x.Estado == EstadoNomina.Inicializada);
                        if (validaNomina != null)
                        {
                            if ((validaNomina.FechaInicio >= request.FechaInicio) && (request.FechaInicio <= validaNomina.FechaFinal))
                            {
                                embargo.Estado = EstadoEmbargo.Vigente;
                            }
                            else
                            {
                                embargo.Estado = EstadoEmbargo.Pendiente;
                            }
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
                #region Actualiza Prioridad
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

                #region Actualiza ConcetoNomina
                //Eliminamos los Conceptos de nómina para este Embargo
                var embargoConceptoNominaBorrar = this.contexto.EmbargoConceptoNominas.Where(x => x.EmbargoId == request.Id)
                                                                                        .Include(x => x.ConceptoNomina)
                                                                                        .ToList();
                foreach (var item in embargoConceptoNominaBorrar)
                {
                    if (item.ConceptoNomina.ClaseConceptoNomina == ClaseConceptoNomina.Devengo)
                    {
                        this.contexto.EmbargoConceptoNominas.Remove(item);
                        await this.contexto.SaveChangesAsync();
                    }
                }

                // Creamos los Conceptos de nómina relacionados
                foreach (var embargoconceptonomina in request.EmbargosConceptoNomina)
                {
                    EmbargoConceptoNomina embargoConceptoNomina = new EmbargoConceptoNomina();
                    embargoConceptoNomina.EmbargoId = embargo.Id;
                    embargoConceptoNomina.ConceptoNominaId = (int)embargoconceptonomina.ConceptoNominaId;

                    this.contexto.EmbargoConceptoNominas.Add(embargoConceptoNomina);
                    await this.contexto.SaveChangesAsync();
                }
                #endregion

                #region Actualiza Subperiodo

                //Eliminamos los Subperiodos Existentes para ese Embargo
                string tabla = typeof(EmbargoSubperiodo).Name;
                this.contexto.Database
                             .ExecuteSqlRaw($"DELETE FROM {tabla} WHERE EmbargoId ={ request.Id}");

                ////Creamos los Subperiodos
                int numeroItem = request.EmbargosSubperiodo.Count();
                int contador = 0;
                if (request.EmbargosSubperiodo.Count != 0)
                {
                    foreach (var item in request.EmbargosSubperiodo)
                    {
                        EmbargoSubperiodo embargoSubperiodo = new EmbargoSubperiodo();
                        embargoSubperiodo.EmbargoId = embargo.Id;
                        embargoSubperiodo.SubPeriodoId = (int)item.SubperiodoId;

                        this.contexto.EmbargoSubperiodos.Add(embargoSubperiodo);
                        await this.contexto.SaveChangesAsync();
                        if (numeroItem == contador)
                        {
                            if (embargoSubperiodo.Embargo.EmbargoSubperiodos != null)
                            {
                                embargoSubperiodo.Embargo.EmbargoSubperiodos = null;
                            }
                        }
                    }

                    embargo.EmbargoSubperiodos.Clear();
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
