using ApiV3.Infraestructura.DbContexto;
using ApiV3.Infraestructura.Enumerador;
using ApiV3.Infraestructura.Resultados;
using ApiV3.Infraestructura.Utilidades;
using ApiV3.Models;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiV3.Dominio.ConceptoNominas.Comandos.Parcial
{
    /// <summary>
    /// Clase encargada de realizar las actualizaciones parciales de ConceptosNomina
    /// </summary>
    public class ParcialConceptoNominaHandler : IRequestHandler<ParcialConceptoNominaRequest, CommandResult>
    {
        private readonly NominaDbContext contexto;
        public ParcialConceptoNominaHandler(NominaDbContext contexto)
        {
            this.contexto = contexto;
        }

        public async Task<CommandResult> Handle(ParcialConceptoNominaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                ConceptoNomina concepto = await this.contexto.ConceptoNominas.FindAsync(request.Id);

                if (request.Codigo != null) concepto.Codigo = request.Codigo.ToUpper();
                if (request.Alias != null) concepto.Alias = Texto.TipoOracion(request.Alias).Replace(" ", "");
                if (request.Nombre != null) concepto.Nombre = Texto.TipoOracion(request.Nombre);
                if (request.TipoConceptoNomina != null) concepto.TipoConceptoNomina = (TipoConceptoNomina)request.TipoConceptoNomina;
                if (request.ClaseConceptoNomina != null) concepto.ClaseConceptoNomina = (ClaseConceptoNomina)request.ClaseConceptoNomina;
                if (request.Orden != null) concepto.Orden = (int)request.Orden;
                if (request.OrigenCentroCosto != null) concepto.OrigenCentroCosto = (OrigenCentroCostoNomina)request.OrigenCentroCosto;
                if (request.OrigenTercero != null) concepto.OrigenTercero = (OrigenTerceroNomina)request.OrigenTercero;
                if (request.VisibleImpresion != null) concepto.VisibleImpresion = (bool)request.VisibleImpresion;
                if (request.UnidadMedida != null) concepto.UnidadMedida = (UnidadMedida)request.UnidadMedida;
                if (request.RequiereCantidad != null) concepto.RequiereCantidad = (bool)request.RequiereCantidad;
                if (request.FuncionNominaId != null) concepto.FuncionNominaId = request.FuncionNominaId;
                if (request.NitTercero != null) concepto.NitTercero = request.NitTercero;
                if (request.DigitoVerificacion != null) concepto.DigitoVerificacion = request.DigitoVerificacion;
                if (request.Descripcion != null) concepto.Descripcion = request.Descripcion;
                #region Estado Registro
                if (request.Activo != null)
                {
                    concepto.EstadoRegistro = EstadoRegistro.Activo;
                    if (request.Activo == false) concepto.EstadoRegistro = EstadoRegistro.Inactivo;
                }
                #endregion


                //Crea la relación entre concepto de nomina tipo administradora
                var conceptoNominaTipoAdministradora = contexto.ConceptoNominaTipoAdministradoras.FirstOrDefault(x => x.ConceptoNominaId == concepto.Id);
                if (conceptoNominaTipoAdministradora != null 
                     && request.TipoAdministradoraId != null 
                     && request.TipoAdministradoraId != conceptoNominaTipoAdministradora.TipoAdministradoraId 
                    )
                {
                    conceptoNominaTipoAdministradora.TipoAdministradoraId = (int)request.TipoAdministradoraId;
                    this.contexto.ConceptoNominaTipoAdministradoras.Update(conceptoNominaTipoAdministradora);
                    await this.contexto.SaveChangesAsync();
                }
                this.contexto.ConceptoNominas.Update(concepto);
                await this.contexto.SaveChangesAsync();
                return CommandResult.Success(concepto);
            }
            catch (System.Exception e)
            {
                return CommandResult.Fail(e.Message);
            }
        }
    }
}