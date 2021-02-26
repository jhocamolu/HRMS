using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ConceptoNominas.Comandos.Actualizar
{
    /// <summary>
    /// Clase encargada de Actualizar los registros de ConceptosNomina
    /// </summary>
    public class ActualizarConceptoNominaHandler : IRequestHandler<ActualizarConceptoNominaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public ActualizarConceptoNominaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(ActualizarConceptoNominaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var concepto = await CreaConcepto(request);

                if (request.ConceptoAgrupador != null)
                {
                    if (request.ConceptoAgrupador == true && request.Bases != null)
                    {
                        List<ConceptoBase> bases = await this.ActualizarBases(request.Bases, concepto.Id);
                    }
                    if (request.ConceptoAgrupador == false && request.Agrupadores != null)
                    {
                        List<ConceptoBase> agrupadores = await this.ActualizarAgrupadores(request.Agrupadores, concepto.Id);
                    }

                }
                //Crea la relación entre concepto de nomina tipo administradora
                if (request.TipoAdministradoraId != null)
                {
                    var conceptoNominaTipoAdministradora = contexto.ConceptoNominaTipoAdministradoras.FirstOrDefault(x => x.ConceptoNominaId == concepto.Id);
                    if (conceptoNominaTipoAdministradora != null)
                    {
                        if (request.TipoAdministradoraId != conceptoNominaTipoAdministradora.TipoAdministradoraId)
                        {
                            conceptoNominaTipoAdministradora.TipoAdministradoraId = (int)request.TipoAdministradoraId;
                            this.contexto.ConceptoNominaTipoAdministradoras.Update(conceptoNominaTipoAdministradora);
                            await this.contexto.SaveChangesAsync();
                        }
                    }else
                    {
                        var conceptoNominaTipoAdministradoraNueva = new ConceptoNominaTipoAdministradora();
                        conceptoNominaTipoAdministradoraNueva.TipoAdministradoraId = (int)request.TipoAdministradoraId;
                        conceptoNominaTipoAdministradoraNueva.ConceptoNominaId = concepto.Id;
                        this.contexto.ConceptoNominaTipoAdministradoras.Add(conceptoNominaTipoAdministradoraNueva);
                        await this.contexto.SaveChangesAsync();
                    }
                }
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(concepto);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.InnerException.Message);
            }
        }

        /// <summary>
        /// Actualiza o crea los conceptos que hacen base para un concepto tipo Agrupador
        /// </summary>
        /// <param name="bases"></param>
        /// <param name="conceptoId"></param>
        /// <returns></returns>
        private async Task<List<ConceptoBase>> ActualizarBases(List<int> bases, int conceptoId)
        {
            List<ConceptoBase> lista = new List<ConceptoBase>();
            try
            {
                foreach (var item in bases)
                {
                    var existeRelacion = contexto.ConceptoBases.FirstOrDefault(x => x.ConceptoNominaId == item && x.ConceptoNominaAgrupadorId == conceptoId);
                    if (existeRelacion == null)
                    {
                        ConceptoBase conceptoBase = new ConceptoBase { };
                        conceptoBase.ConceptoNominaId = item;
                        conceptoBase.ConceptoNominaAgrupadorId = conceptoId;
                        this.contexto.ConceptoBases.Add(conceptoBase);
                    }
                    else if (existeRelacion != null)
                    {
                        existeRelacion.EstadoRegistro = EstadoRegistro.Activo;
                        this.contexto.ConceptoBases.Update(existeRelacion);
                    }
                }
                await this.contexto.SaveChangesAsync();


                await this.InactivarRelacion(bases, conceptoId, true);

                lista.AddRange(await contexto.ConceptoBases.Where(x => x.ConceptoNominaAgrupadorId == conceptoId && x.EstadoRegistro == EstadoRegistro.Activo).ToListAsync());
                return lista;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Actualiza o crea la relacion de los conceptos tipo agrupador con el concepto de nomina seleccionado
        /// </summary>
        /// <param name="agrupadores"></param>
        /// <param name="conceptoId"></param>
        /// <returns></returns>
        private async Task<List<ConceptoBase>> ActualizarAgrupadores(List<int> agrupadores, int conceptoId)
        {
            List<ConceptoBase> lista = new List<ConceptoBase>();
            try
            {
                foreach (var item in agrupadores)
                {
                    var existeRelacion = contexto.ConceptoBases.FirstOrDefault(x => x.ConceptoNominaId == conceptoId && x.ConceptoNominaAgrupadorId == item);
                    if (existeRelacion == null)
                    {
                        ConceptoBase conceptoBase = new ConceptoBase { };
                        conceptoBase.ConceptoNominaId = conceptoId;
                        conceptoBase.ConceptoNominaAgrupadorId = item;
                        this.contexto.ConceptoBases.Add(conceptoBase);
                    }
                    else if (existeRelacion != null)
                    {
                        existeRelacion.EstadoRegistro = EstadoRegistro.Activo;
                        this.contexto.ConceptoBases.Update(existeRelacion);
                    }
                }
                await this.contexto.SaveChangesAsync();
                lista.AddRange(await contexto.ConceptoBases.Where(x => x.ConceptoNominaId == conceptoId && x.EstadoRegistro == EstadoRegistro.Activo).ToListAsync());
                await this.InactivarRelacion(agrupadores, conceptoId, false);
                return lista;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Inactivar relacion
        /// </summary>
        /// <param name="lista"></param>
        /// <param name="conceptoId"></param>
        /// <param name="esAgrupador"></param>
        /// <returns></returns>
        private async Task<bool> InactivarRelacion(List<int> lista, int conceptoId, bool esAgrupador)
        {
            try
            {
                List<ConceptoBase> conceptoBase = new List<ConceptoBase>();
                if (esAgrupador)
                {
                    conceptoBase = contexto.ConceptoBases.Where(c => c.ConceptoNominaAgrupadorId == conceptoId).ToList();
                    foreach (var item in conceptoBase)
                    {
                        if (!lista.Contains(item.ConceptoNominaId))
                        {
                            item.EstadoRegistro = EstadoRegistro.Inactivo;
                            this.contexto.ConceptoBases.Update(item);
                        }
                    }
                }
                else
                {
                    conceptoBase = contexto.ConceptoBases.Where(c => c.ConceptoNominaId == conceptoId).ToList();
                    foreach (var item in conceptoBase)
                    {
                        if (!lista.Contains(item.ConceptoNominaAgrupadorId))
                        {
                            item.EstadoRegistro = EstadoRegistro.Inactivo;
                            this.contexto.ConceptoBases.Update(item);
                        }
                    }
                }
                await this.contexto.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        /// <summary>
        /// Actualiza el concepto de la nomina
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ConceptoNomina> CreaConcepto(ActualizarConceptoNominaRequest request)
        {
            ConceptoNomina concepto = await this.contexto.ConceptoNominas.FindAsync(request.Id);


            concepto.Codigo = request.Codigo.ToUpper();
            concepto.Alias = Texto.TipoOracion(request.Alias).Replace(" ", "");
            concepto.Nombre = Texto.TipoOracion(request.Nombre);
            concepto.TipoConceptoNomina = (TipoConceptoNomina)request.TipoConceptoNomina;
            concepto.ClaseConceptoNomina = (ClaseConceptoNomina)request.ClaseConceptoNomina;
            //concepto.Orden = (int)request.Orden;
            concepto.OrigenCentroCosto = request.OrigenCentroCosto;
            concepto.OrigenTercero = request.OrigenTercero;
            concepto.VisibleImpresion = (bool)request.VisibleImpresion;
            concepto.UnidadMedida = request.UnidadMedida;
            concepto.RequiereCantidad = (bool)request.RequiereCantidad;
            concepto.FuncionNominaId = request.FuncionNominaId;
            if (request.NitTercero != null) concepto.NitTercero = request.NitTercero;
            if (request.DigitoVerificacion != null) concepto.DigitoVerificacion = request.DigitoVerificacion;
            concepto.Descripcion = request.Descripcion;
            concepto.ConceptoAgrupador = (bool)request.ConceptoAgrupador;
            this.contexto.ConceptoNominas.Update(concepto);
            await this.contexto.SaveChangesAsync();
            return concepto;
        }
    }
}
