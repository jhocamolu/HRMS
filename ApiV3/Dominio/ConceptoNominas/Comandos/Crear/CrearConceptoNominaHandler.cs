using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ConceptoNominas.Comandos.Crear
{
    /// <summary>
    /// Clase encargada de crear los registros de ConceptosNomina
    /// </summary>
    public class CrearConceptoNominaHandler : IRequestHandler<CrearConceptoNominaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;

        public CrearConceptoNominaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }
        public async Task<CommandResult> Handle(CrearConceptoNominaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var concepto = await CreaConcepto(request);
                if (request.ConceptoAgrupador != null)
                {
                    if (request.ConceptoAgrupador == true)
                    {
                        List<ConceptoBase> listaBases = await this.CrearBases(request.Bases, concepto.Id);
                    }
                    if (request.ConceptoAgrupador == false)
                    {
                        List<ConceptoBase> listaAgrupadores = await this.CrearAgrupadores(request.Agrupadores, concepto.Id);
                    }
                }

                //Crea la relación entre concepto de nomina tipo administradora
                if (request.TipoAdministradoraId != null )
                {
                    var conceptoNominaTipoAdministradora = new ConceptoNominaTipoAdministradora();
                    conceptoNominaTipoAdministradora.TipoAdministradoraId = (int)request.TipoAdministradoraId;
                    conceptoNominaTipoAdministradora.ConceptoNominaId = concepto.Id;
                    this.contexto.ConceptoNominaTipoAdministradoras.Add(conceptoNominaTipoAdministradora);
                    await this.contexto.SaveChangesAsync();
                }
                return CommandResult.Success(concepto);
            }
            catch (Exception e)
            {
                return CommandResult.Fail(e.InnerException.Message);
            }
        }

        private async Task<List<ConceptoBase>> CrearBases(List<int> bases, int conceptoId)
        {
            List<ConceptoBase> lista = new List<ConceptoBase>();
            foreach (var item in bases)
            {
                lista.Add(new ConceptoBase
                {
                    ConceptoNominaId = item,
                    ConceptoNominaAgrupadorId = conceptoId
                });

            }
            this.contexto.ConceptoBases.AddRange(lista);
            await this.contexto.SaveChangesAsync();
            return lista;
        }

        private async Task<List<ConceptoBase>> CrearAgrupadores(List<int> agrupadores, int conceptoId)
        {
            List<ConceptoBase> lista = new List<ConceptoBase>();
            foreach (var item in agrupadores)
            {
                lista.Add(new ConceptoBase
                {
                    ConceptoNominaId = conceptoId,
                    ConceptoNominaAgrupadorId = item
                });

            }
            this.contexto.ConceptoBases.AddRange(lista);
            await this.contexto.SaveChangesAsync();
            return lista;
        }
        private async Task<ConceptoNomina> CreaConcepto(CrearConceptoNominaRequest request)
        {
            int orden = contexto.ConceptoNominas.Max(x => x.Orden) + 1;
            ConceptoNomina concepto = new ConceptoNomina { };
            concepto.Codigo = request.Codigo.ToUpper();
            concepto.Alias = Texto.TipoOracion(request.Alias).Replace(" ", "");
            concepto.Nombre = Texto.TipoOracion(request.Nombre);
            concepto.TipoConceptoNomina = (TipoConceptoNomina)request.TipoConceptoNomina;
            concepto.ClaseConceptoNomina = (ClaseConceptoNomina)request.ClaseConceptoNomina;
            concepto.Orden = orden;
            concepto.ConceptoAgrupador = (bool)request.ConceptoAgrupador;
            concepto.OrigenCentroCosto = request.OrigenCentroCosto;
            concepto.OrigenTercero = request.OrigenTercero;
            concepto.VisibleImpresion = (bool)request.VisibleImpresion;
            concepto.UnidadMedida = request.UnidadMedida;
            concepto.RequiereCantidad = (bool)request.RequiereCantidad;
            concepto.FuncionNominaId = request.FuncionNominaId;
            if (request.NitTercero != null) concepto.NitTercero = request.NitTercero;
            if (request.DigitoVerificacion != null) concepto.DigitoVerificacion = request.DigitoVerificacion;
            concepto.Descripcion = request.Descripcion;
            this.contexto.ConceptoNominas.Add(concepto);
            await this.contexto.SaveChangesAsync();
            return concepto;
        }
    }
}
